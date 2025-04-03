using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Models;

namespace VaccinaCare.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SystemController : ControllerBase
{
    private readonly VaccinaCareDbContext _context;

    public SystemController(VaccinaCareDbContext context)
    {
        _context = context;
    }

    [HttpPost("seed-all-data")]
    public async Task<IActionResult> SeedData()
    {
        try
        {
            await ClearDatabase(_context);

            // Seed Users
            var users = new List<User>
            {
                new()
                {
                    FullName = "Admin User",
                    Email = "a@gmail.com",
                    Gender = true,
                    DateOfBirth = new DateTime(1990, 1, 1),
                    PhoneNumber = "0987654321",
                    PasswordHash = "1@",
                    ImageUrl = "",
                    RoleName = "Admin",
                    RoleId = 1,
                    Address = "123 Admin Street, Hanoi",
                    IsDeleted = false,
                    CreatedAt = DateTime.UtcNow,
                },
                new()
                {
                    FullName = "Staff Member",
                    Email = "s@gmail.com",
                    Gender = false,
                    DateOfBirth = new DateTime(1995, 5, 15),
                    PhoneNumber = "0912345678",
                    ImageUrl = "",
                    PasswordHash = "1@",
                    RoleId = 2,
                    RoleName = "Staff",
                    Address = "456 Staff Avenue, Ho Chi Minh City",
                    IsDeleted = false,
                    CreatedAt = DateTime.UtcNow,
                },
                new()
                {
                    FullName = "Customer One",
                    Email = "c@gmail.com",
                    Gender = true,
                    DateOfBirth = new DateTime(1992, 8, 20),
                    RoleId = 3,
                    PhoneNumber = "0923456789",
                    ImageUrl = "",
                    PasswordHash = "1@",
                    RoleName = "Customer",
                    Address = "789 Customer Blvd, Da Nang",
                    IsDeleted = false,
                    CreatedAt = DateTime.UtcNow,
                },
                new()
                {
                    FullName = "Customer Two",
                    Email = "customer2@example.com",
                    Gender = false,
                    DateOfBirth = new DateTime(1988, 3, 10),
                    PhoneNumber = "0934567890",
                    ImageUrl = "",
                    PasswordHash = "1@",
                    RoleName = "Customer",
                    Address = "101 Customer Lane, Can Tho",
                    IsDeleted = false,
                    CreatedAt = DateTime.UtcNow,
                }
            };

            await _context.Users.AddRangeAsync(users);
            await _context.SaveChangesAsync();


            #region Seed VaccineTypes
            var vaccineTypes = new List<VaccineType>
            {
                new() { Name = "Routine", Description = "Vaccines recommended for routine use in children and adults" },
                new() { Name = "Travel", Description = "Vaccines for travelers to specific regions" },
                new()
                {
                    Name = "Optional", Description = "Optional vaccines that may be recommended for certain individuals"
                },
                new() { Name = "Seasonal", Description = "Vaccines administered seasonally like flu vaccines" },
                new()
                {
                    Name = "Special Conditions", Description = "Vaccines for people with special health conditions"
                }
            };

            await _context.VaccineTypes.AddRangeAsync(vaccineTypes);
            await _context.SaveChangesAsync();
            #endregion
            
            #region Seed Vaccines

            // Get the IDs of the inserted vaccine types for reference
            var routineTypeId = vaccineTypes.First(t => t.Name == "Routine").Id;
            var travelTypeId = vaccineTypes.First(t => t.Name == "Travel").Id;
            var optionalTypeId = vaccineTypes.First(t => t.Name == "Optional").Id;
            var seasonalTypeId = vaccineTypes.First(t => t.Name == "Seasonal").Id;
            var specialConditionsTypeId = vaccineTypes.First(t => t.Name == "Special Conditions").Id;

            var vaccines = new List<Vaccine>
            {
                new()
                {
                    VaccineName = "BCG",
                    Description = "Phòng lao",
                    Price = 150000,
                    PicUrl =
                        "https://minio.ae-tao-fullstack-api.site/api/v1/buckets/vaccinacare-bucket/objects/download?preview=true&prefix=vaccines%2FBCG.jpg&version_id=null",
                    RequiredDoses = 1,
                    DoseIntervalDays = 0,
                    AvoidChronic = true,
                    AvoidAllergy = false,
                    HasDrugInteraction = false,
                    HasSpecialWarning = false,
                    VaccineTypeId = routineTypeId
                },
                new()
                {
                    VaccineName = "Pentaxim",
                    Description = "Bạch hầu, Ho gà, Uốn ván, Bại liệt, Hib",
                    Price = 795000,
                    PicUrl =
                        "https://minio.ae-tao-fullstack-api.site/api/v1/buckets/vaccinacare-bucket/objects/download?preview=true&prefix=vaccines%2FPentaxim.jpg&version_id=null",
                    RequiredDoses = 3,
                    DoseIntervalDays = 30,
                    AvoidChronic = false,
                    AvoidAllergy = true,
                    HasDrugInteraction = false,
                    HasSpecialWarning = false,
                    VaccineTypeId = routineTypeId
                },
                new()
                {
                    VaccineName = "Infanrix Hexa",
                    Description = "6 trong 1 (DTP, Bại liệt, Hib, Viêm gan B)",
                    Price = 1015000,
                    PicUrl =
                        "https://minio.ae-tao-fullstack-api.site/api/v1/buckets/vaccinacare-bucket/objects/download?preview=true&prefix=vaccines%2FInfanrix%20Hexa.jpg&version_id=null",
                    RequiredDoses = 3,
                    DoseIntervalDays = 28,
                    AvoidChronic = false,
                    AvoidAllergy = true,
                    HasDrugInteraction = false,
                    HasSpecialWarning = false,
                    VaccineTypeId = routineTypeId
                },
                new()
                {
                    VaccineName = "Rotateq",
                    Description = "Ngừa tiêu chảy do Rotavirus",
                    Price = 665000,
                    PicUrl =
                        "https://minio.ae-tao-fullstack-api.site/api/v1/buckets/vaccinacare-bucket/objects/download?preview=true&prefix=vaccines%2FRotateq.jpg&version_id=null",
                    RequiredDoses = 3,
                    DoseIntervalDays = 42,
                    AvoidChronic = true,
                    AvoidAllergy = true,
                    HasDrugInteraction = false,
                    HasSpecialWarning = false,
                    VaccineTypeId = routineTypeId
                },
                new()
                {
                    VaccineName = "IPV",
                    Description = "Bại liệt (tiêm)",
                    Price = 450000,
                    PicUrl =
                        "https://minio.ae-tao-fullstack-api.site/api/v1/buckets/vaccinacare-bucket/objects/download?preview=true&prefix=vaccines%2FIPV.png&version_id=null",
                    RequiredDoses = 4,
                    DoseIntervalDays = 60,
                    AvoidChronic = false,
                    AvoidAllergy = false,
                    HasDrugInteraction = false,
                    HasSpecialWarning = false,
                    VaccineTypeId = routineTypeId
                },
                new()
                {
                    VaccineName = "OPV",
                    Description = "Bại liệt (uống)",
                    Price = 100000,
                    PicUrl =
                        "https://minio.ae-tao-fullstack-api.site/api/v1/buckets/vaccinacare-bucket/objects/download?preview=true&prefix=vaccines%2FOPV.png&version_id=null",
                    RequiredDoses = 4,
                    DoseIntervalDays = 30,
                    AvoidChronic = false,
                    AvoidAllergy = false,
                    HasDrugInteraction = false,
                    HasSpecialWarning = false,
                    VaccineTypeId = routineTypeId
                },
                new()
                {
                    VaccineName = "Measles (MVVac)",
                    Description = "Sởi đơn",
                    Price = 396000,
                    RequiredDoses = 2,
                    DoseIntervalDays = 90,
                    PicUrl =
                        "https://minio.ae-tao-fullstack-api.site/api/v1/buckets/vaccinacare-bucket/objects/download?preview=true&prefix=vaccines%2FOPV.png&version_id=null",
                    AvoidChronic = false,
                    AvoidAllergy = true,
                    HasDrugInteraction = false,
                    HasSpecialWarning = false,
                    VaccineTypeId = routineTypeId
                },
                new()
                {
                    VaccineName = "MMR II",
                    Description = "Sởi - Quai bị - Rubella",
                    Price = 445000,
                    RequiredDoses = 2,
                    DoseIntervalDays = 180,
                    PicUrl =
                        "https://minio.ae-tao-fullstack-api.site/api/v1/buckets/vaccinacare-bucket/objects/download?preview=true&prefix=vaccines%2FMMR.jpg&version_id=null",
                    AvoidChronic = false,
                    AvoidAllergy = true,
                    HasDrugInteraction = false,
                    HasSpecialWarning = false,
                    VaccineTypeId = routineTypeId
                },
                new()
                {
                    VaccineName = "Varivax",
                    Description = "Thủy đậu",
                    Price = 1085000,
                    RequiredDoses = 2,
                    DoseIntervalDays = 90,
                    PicUrl =
                        "https://minio.ae-tao-fullstack-api.site/api/v1/buckets/vaccinacare-bucket/objects/download?preview=true&prefix=vaccines%2FVARIVAX.jpg&version_id=null",
                    AvoidChronic = true,
                    AvoidAllergy = true,
                    HasDrugInteraction = false,
                    HasSpecialWarning = false,
                    VaccineTypeId = optionalTypeId
                },
                new()
                {
                    VaccineName = "Havrix",
                    Description = "Viêm gan A",
                    Price = 850000,
                    RequiredDoses = 2,
                    DoseIntervalDays = 180,
                    PicUrl =
                        "https://minio.ae-tao-fullstack-api.site/api/v1/buckets/vaccinacare-bucket/objects/download?preview=true&prefix=vaccines%2FHavrix.jpg&version_id=null",
                    AvoidChronic = false,
                    AvoidAllergy = false,
                    HasDrugInteraction = false,
                    HasSpecialWarning = false,
                    VaccineTypeId = optionalTypeId
                },
                new()
                {
                    VaccineName = "Ixiaro",
                    Description = "Viêm não Nhật Bản",
                    Price = 1300000,
                    RequiredDoses = 2,
                    DoseIntervalDays = 28,
                    PicUrl =
                        "https://minio.ae-tao-fullstack-api.site/api/v1/buckets/vaccinacare-bucket/objects/download?preview=true&prefix=vaccines%2FIxiaro.jpg&version_id=null",
                    AvoidChronic = false,
                    AvoidAllergy = true,
                    HasDrugInteraction = false,
                    HasSpecialWarning = false,
                    VaccineTypeId = routineTypeId
                },
                new()
                {
                    VaccineName = "Typhim Vi",
                    Description = "Thương hàn",
                    Price = 900000,
                    RequiredDoses = 1,
                    DoseIntervalDays = 0,
                    PicUrl =
                        "https://minio.ae-tao-fullstack-api.site/api/v1/buckets/vaccinacare-bucket/objects/download?preview=true&prefix=vaccines%2FTyphim%20Vi.jpg&version_id=null",
                    AvoidChronic = false,
                    AvoidAllergy = false,
                    HasDrugInteraction = false,
                    HasSpecialWarning = false,
                    VaccineTypeId = travelTypeId
                },
                new()
                {
                    VaccineName = "Verorab",
                    Description = "Dại",
                    Price = 950000,
                    RequiredDoses = 4,
                    DoseIntervalDays = 7,
                    PicUrl =
                        "https://minio.ae-tao-fullstack-api.site/api/v1/buckets/vaccinacare-bucket/objects/download?preview=true&prefix=vaccines%2FVerorab.jpg&version_id=null",
                    AvoidChronic = false,
                    AvoidAllergy = true,
                    HasDrugInteraction = false,
                    HasSpecialWarning = false,
                    VaccineTypeId = specialConditionsTypeId
                },
                new()
                {
                    VaccineName = "Menactra",
                    Description = "Viêm màng não mô cầu",
                    Price = 1750000,
                    RequiredDoses = 1,
                    DoseIntervalDays = 0,
                    PicUrl =
                        "https://minio.ae-tao-fullstack-api.site/api/v1/buckets/vaccinacare-bucket/objects/download?preview=true&prefix=vaccines%2FMenactra.jpg&version_id=null",
                    AvoidChronic = false,
                    AvoidAllergy = true,
                    HasDrugInteraction = false,
                    HasSpecialWarning = false,
                    VaccineTypeId = travelTypeId
                },
                new()
                {
                    VaccineName = "Gardasil",
                    Description = "HPV (Ngừa ung thư cổ tử cung)",
                    Price = 1790000,
                    RequiredDoses = 2,
                    DoseIntervalDays = 180,
                    PicUrl =
                        "https://minio.ae-tao-fullstack-api.site/api/v1/buckets/vaccinacare-bucket/objects/download?preview=true&prefix=vaccines%2FGardasil.png&version_id=null",
                    AvoidChronic = false,
                    AvoidAllergy = false,
                    HasDrugInteraction = false,
                    HasSpecialWarning = false,
                    VaccineTypeId = optionalTypeId
                },
                new()
                {
                    VaccineName = "Vaxigrip Tetra",
                    Description = "Cúm mùa",
                    Price = 356000,
                    RequiredDoses = 1,
                    DoseIntervalDays = 0,
                    PicUrl =
                        "https://minio.ae-tao-fullstack-api.site/api/v1/buckets/vaccinacare-bucket/objects/download?preview=true&prefix=vaccines%2FVaxigrip.jpg&version_id=null",
                    AvoidChronic = false,
                    AvoidAllergy = true,
                    HasDrugInteraction = false,
                    HasSpecialWarning = false,
                    VaccineTypeId = seasonalTypeId
                },
                new()
                {
                    VaccineName = "Pfizer-BioNTech COVID-19",
                    Description = "COVID-19 (5+)",
                    Price = 1200000,
                    RequiredDoses = 2,
                    DoseIntervalDays = 21,
                    PicUrl =
                        "https://minio.ae-tao-fullstack-api.site/api/v1/buckets/vaccinacare-bucket/objects/download?preview=true&prefix=vaccines%2FPfizer-BioNTech%20COVID-19.png&version_id=null",
                    AvoidChronic = false,
                    AvoidAllergy = true,
                    HasDrugInteraction = false,
                    HasSpecialWarning = true,
                    VaccineTypeId = seasonalTypeId
                },
                new()
                {
                    VaccineName = "Hexaxim",
                    Description = "6 trong 1 (DTP, Bại liệt, Hib, Viêm gan B)",
                    Price = 950000,
                    PicUrl =
                        "https://minio.ae-tao-fullstack-api.site/api/v1/buckets/vaccinacare-bucket/objects/download?preview=true&prefix=vaccines%2FHexaxim.jpg&version_id=null",
                    RequiredDoses = 3,
                    DoseIntervalDays = 28,
                    AvoidChronic = false,
                    AvoidAllergy = true,
                    HasDrugInteraction = false,
                    HasSpecialWarning = false,
                    VaccineTypeId = routineTypeId
                },
                new()
                {
                    VaccineName = "Rotarix",
                    Description = "Ngừa tiêu chảy do Rotavirus",
                    Price = 650000,
                    PicUrl =
                        "https://minio.ae-tao-fullstack-api.site/api/v1/buckets/vaccinacare-bucket/objects/download?preview=true&prefix=vaccines%2FRotarix.jpg&version_id=null",
                    RequiredDoses = 2,
                    DoseIntervalDays = 28,
                    AvoidChronic = true,
                    AvoidAllergy = true,
                    HasDrugInteraction = false,
                    HasSpecialWarning = false,
                    VaccineTypeId = routineTypeId
                },
                new()
                {
                    VaccineName = "Synflorix",
                    Description = "Viêm phổi do phế cầu khuẩn (PCV13)",
                    Price = 1200000,
                    PicUrl =
                        "https://minio.ae-tao-fullstack-api.site/api/v1/buckets/vaccinacare-bucket/objects/download?preview=true&prefix=vaccines%2FSynflorix.jpg&version_id=null",
                    RequiredDoses = 3,
                    DoseIntervalDays = 60,
                    AvoidChronic = false,
                    AvoidAllergy = true,
                    HasDrugInteraction = false,
                    HasSpecialWarning = false,
                    VaccineTypeId = routineTypeId
                },
                new()
                {
                    VaccineName = "Priorix",
                    Description = "Sởi, Quai bị, Rubella",
                    Price = 850000,
                    PicUrl =
                        "https://minio.ae-tao-fullstack-api.site/api/v1/buckets/vaccinacare-bucket/objects/download?preview=true&prefix=vaccines%2FPriorix.jpg&version_id=null",
                    RequiredDoses = 2,
                    DoseIntervalDays = 180,
                    AvoidChronic = false,
                    AvoidAllergy = true,
                    HasDrugInteraction = false,
                    HasSpecialWarning = false,
                    VaccineTypeId = routineTypeId
                }
            };

            await _context.Vaccines.AddRangeAsync(vaccines);
            await _context.SaveChangesAsync();

            #endregion
            

            return Ok(new { Message = "All data seeded successfully" });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new { Error = e.Message });
        }
    }

    private async Task ClearDatabase(VaccinaCareDbContext context)
    {
        using var transaction = await context.Database.BeginTransactionAsync();

        try
        {
            var tablesToDelete = new List<Func<Task>>
            {
                () => context.Users.ExecuteDeleteAsync(),
                () => context.Vaccines.ExecuteDeleteAsync(),
                () => context.VaccineTypes.ExecuteDeleteAsync()
            };

            foreach (var deleteFunc in tablesToDelete)
                await deleteFunc();

            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}