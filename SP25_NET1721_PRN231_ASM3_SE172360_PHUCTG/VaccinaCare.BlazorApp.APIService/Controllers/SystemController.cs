using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaccinaCare.Application.Interface.Common;
using VaccinaCare.Application.Ultils;
using VaccinaCare.Domain;
using VaccinaCare.Domain.Entities;

namespace VaccinaCare.BlazorApp.APIService.Controllers
{
    [ApiController]
    [Route("api/system")]
    public class SystemController : ControllerBase
    {
        private readonly VaccinaCareDbContext _context;
        private readonly ILoggerService _logger;

        public SystemController(VaccinaCareDbContext context, ILoggerService logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost("seed-all-data")]
        [ProducesResponseType(typeof(ApiResult<object>), 200)]
        [ProducesResponseType(typeof(ApiResult<object>), 400)]
        [ProducesResponseType(typeof(ApiResult<object>), 500)]
        public async Task<IActionResult> SeedData()
        {
            try
            {
                await ClearDatabase(_context);

                // Seed data
                var feedbacks = await SeedFeedbacks();

                return Ok(ApiResult<object>.Success(new
                {
                    Feedbacks = feedbacks.Count,
                    Message = "Data seeded successfully.",
                }));
            }
            catch (DbUpdateException dbEx)
            {
                _logger.Error($"Database update error: {dbEx.Message}");
                return StatusCode(500, "Error seeding data: Database issue.");
            }
            catch (Exception ex)
            {
                _logger.Error($"General error: {ex.Message}");
                return StatusCode(500, "Error seeding data: General failure.");
            }
        }

        private async Task<List<Feedback>> SeedFeedbacks()
        {
            // Ensure FeedbackTypes exist before seeding Feedbacks
            var feedbackTypes = await _context.FeedbackTypes.ToListAsync();

            if (!feedbackTypes.Any())
            {
                feedbackTypes = new List<FeedbackType>
        {
            new FeedbackType { Id = Guid.NewGuid(), Name = "Positive" },
            new FeedbackType { Id = Guid.NewGuid(), Name = "Negative" },
            new FeedbackType { Id = Guid.NewGuid(), Name = "Neutral" }
        };

                await _context.FeedbackTypes.AddRangeAsync(feedbackTypes);
                await _context.SaveChangesAsync();
            }

            var positiveTypeId = feedbackTypes.FirstOrDefault(ft => ft.Name == "Positive")?.Id ?? Guid.NewGuid();
            var negativeTypeId = feedbackTypes.FirstOrDefault(ft => ft.Name == "Negative")?.Id ?? Guid.NewGuid();
            var neutralTypeId = feedbackTypes.FirstOrDefault(ft => ft.Name == "Neutral")?.Id ?? Guid.NewGuid();

            var feedbacks = new List<Feedback>
    {
        new Feedback
        {
            Rating = 5,
            Comments = "Great service!",
            FeedbackTypeId = positiveTypeId
        },
        new Feedback
        {
            Rating = 2,
            Comments = "Not satisfied with the service.",
            FeedbackTypeId = negativeTypeId
        },
        new Feedback
        {
            Rating = 3,
            Comments = "It was okay.",
            FeedbackTypeId = neutralTypeId
        }
    };

            await _context.Feedbacks.AddRangeAsync(feedbacks);
            await _context.SaveChangesAsync();

            return feedbacks;
        }



        private async Task ClearDatabase(VaccinaCareDbContext context)
        {
            using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                _logger.Info("Bắt đầu xóa dữ liệu trong database...");

                // Danh sách các bảng cần xóa (theo thứ tự quan hệ FK)
                var tablesToDelete = new List<Func<Task>>
                {
                    () => context.Notifications.ExecuteDeleteAsync(),
                    () => context.AppointmentsServices.ExecuteDeleteAsync(),
                    () => context.Appointments.ExecuteDeleteAsync(),
                    () => context.CancellationPolicies.ExecuteDeleteAsync(),
                    () => context.Children.ExecuteDeleteAsync(),
                    () => context.Feedbacks.ExecuteDeleteAsync(),
                    () => context.Invoices.ExecuteDeleteAsync(),
                    () => context.PackageProgresses.ExecuteDeleteAsync(),
                    () => context.Payments.ExecuteDeleteAsync(),
                    () => context.UsersVaccinationServices.ExecuteDeleteAsync(),
                    () => context.VaccinationRecords.ExecuteDeleteAsync(),
                    () => context.VaccineSuggestions.ExecuteDeleteAsync(),

                    // Delete VaccinePackageDetail first, then VaccinePackage
                    () => context.VaccinePackageDetails.ExecuteDeleteAsync(), // Child
                    () => context.VaccinePackages.ExecuteDeleteAsync(), // Parent

                    () => context.ServiceAvailabilities.ExecuteDeleteAsync(),
                    () => context.Vaccines.ExecuteDeleteAsync(),
                    () => context.Users.ExecuteDeleteAsync(),
                    () => context.Roles.ExecuteDeleteAsync(),
                };


                foreach (var deleteFunc in tablesToDelete)
                {
                    await deleteFunc();
                }

                await transaction.CommitAsync();
                _logger.Success("Xóa sạch dữ liệu trong database thành công.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.Error($"Xóa dữ liệu thất bại: {ex.Message}");
                throw;
            }
        }
    }


}
