using VaccinaCare.Application.Interface;
using VaccinaCare.Application.Interface.Common;
using VaccinaCare.Domain.DTOs.FeedBackDTOs;
using VaccinaCare.Domain.Entities;
using VaccinaCare.Domain.Enums;
using VaccinaCare.Repository.Interfaces;

namespace VaccinaCare.Application.Service
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerService _logger;
        private readonly IClaimsService _claimsService;
        public FeedbackService(IUnitOfWork unitOfWork, ILoggerService logger, IClaimsService claimsService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _claimsService = claimsService;
        }

        public async Task<FeedbackDto> CreateFeedbackAsync(FeedbackDto feedbackDto)
        {
            try
            {
                Guid userId = _claimsService.GetCurrentUserId;
                _logger.Info($"User {userId} is attepting to create feedback.");

                var appointment = await _unitOfWork.AppointmentRepository.GetByIdAsync(feedbackDto.AppointmentId);

                if (appointment == null)
                {
                    _logger.Warn($"Appointment {feedbackDto.AppointmentId} not found");
                    throw new KeyNotFoundException("Appointment not found.");
                }

                if (appointment.ParentId != userId)
                {
                    _logger.Warn(($"User {userId} is not authorized to leave feedback for Appointment {feedbackDto.AppointmentId}."));
                    throw new UnauthorizedAccessException("You are not authorized to give feedback for this appointment.");
                }

                if (appointment.Status != AppointmentStatus.Completed)
                {
                    _logger.Warn($"Appointment {feedbackDto.AppointmentId} is not completed. Feedback not allowed.");
                    throw new InvalidOperationException("Feedback can only be given for completed appointments.");
                }

                if (feedbackDto.Rating < 1 || feedbackDto.Rating > 5)
                {
                    _logger.Warn($"Invalid rating: {feedbackDto.Rating}. Rating must be between 1 and 5.");
                    throw new ArgumentOutOfRangeException(nameof(feedbackDto.Rating), "Rating must be between 1 and 5.");
                }


                var feedback = new Feedback
                {
                    AppointmentId = feedbackDto.AppointmentId,
                    Rating = feedbackDto.Rating,
                    Comments = feedbackDto.Comments ?? "No comments provided.",
                    CreatedAt = DateTime.UtcNow
                };

                await _unitOfWork.FeedbackRepository.AddAsync(feedback);
                await _unitOfWork.SaveChangesAsync();

                _logger.Info($"User {userId} added feedback for Appointment {feedbackDto.AppointmentId}");

                return new FeedbackDto
                {
                    AppointmentId = feedback.AppointmentId.GetValueOrDefault(),
                    Rating = feedback.Rating.GetValueOrDefault(),
                    Comments = feedback.Comments
                };
            }
            catch (Exception ex)
            {
                _logger.Error($"Error in CreateFeedbackAsync: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteFeedbackAsync(Guid feedbackId)
        {
            {
                try
                {
                    _logger.Info($"Attempting to delete feedback {feedbackId}.");

                    var feedback = await _unitOfWork.FeedbackRepository.GetByIdAsync(feedbackId);

                    if (feedback == null)
                    {
                        _logger.Warn($"Feedback {feedbackId} not found.");
                        throw new KeyNotFoundException("Feedback not found.");
                    }

                    await _unitOfWork.FeedbackRepository.SoftRemove(feedback);
                    await _unitOfWork.SaveChangesAsync();

                    _logger.Info($"Successfully deleted feedback {feedbackId}.");
                }
                catch (Exception ex)
                {
                    _logger.Error($"An error occurred while deleting feedback {feedbackId}: {ex.Message}");
                    throw;
                }
            }
        }

        public async Task<List<FeedbackDto>> GetAllFeedbacksAsync()
        {
            try
            {
                var feedbacks = await _unitOfWork.FeedbackRepository.GetAllAsync();

                var feedbackDtos = feedbacks.Select(feedback => new FeedbackDto
                {
                    AppointmentId = feedback.AppointmentId.GetValueOrDefault(),
                    Rating = feedback.Rating.GetValueOrDefault(),
                    Comments = feedback.Comments
                }).ToList();

                return feedbackDtos;
            }
            catch (Exception ex)
            {
                _logger.Error($"Error while fetching feedbacks: {ex.Message}");
                throw new Exception("An error occurred while fetching feedbacks. Please try again later");
            }
        }

        public async Task<FeedbackDto> GetFeedbackByIdAsync(Guid feedbackId)
        {
            try
            {
                _logger.Info($"Fetching feedback with ID: {feedbackId}");

                var feedback = await _unitOfWork.FeedbackRepository.GetByIdAsync(feedbackId);

                if (feedback == null)
                {
                    _logger.Warn($"Feedback {feedbackId} not found.");
                    throw new KeyNotFoundException("Feedback not found.");
                }

                return new FeedbackDto
                {
                    AppointmentId = feedback.AppointmentId.GetValueOrDefault(),
                    Rating = feedback.Rating.GetValueOrDefault(),
                    Comments = feedback.Comments
                };
            }
            catch (Exception ex)
            {
                _logger.Error($"An error occurred while fetching feedback {feedbackId}");
                throw;
            }
        }

        public async Task<FeedbackDto> UpdateFeedbackAsync(Guid feedbackId, FeedbackDto feedbackDto)
        {
            try
            {
                Guid userId = _claimsService.GetCurrentUserId;
                _logger.Info($"User {userId} is attempting to update feedback {feedbackId}.");

                var feedback = await _unitOfWork.FeedbackRepository.GetByIdAsync(feedbackId);

                if (feedback == null)
                {
                    _logger.Warn($"Feedback {feedbackId} not found.");
                    throw new KeyNotFoundException("Feedback not found.");
                }

                var appointment = await _unitOfWork.AppointmentRepository.GetByIdAsync(feedback.AppointmentId.GetValueOrDefault());

                if (appointment == null)
                {
                    _logger.Warn($"Appointment {feedback.AppointmentId} not found.");
                    throw new KeyNotFoundException("Appointment not found.");
                }

                if (appointment.ParentId.GetValueOrDefault() != userId)
                {
                    _logger.Warn($"User {userId} is not authorized to update feedback for Appointment {feedback.AppointmentId}.");
                    throw new UnauthorizedAccessException("You are not authorized to update this feedback.");
                }

                if (appointment.Status != AppointmentStatus.Completed)
                {
                    _logger.Warn($"Appointment {feedback.AppointmentId} is not completed. Feedback cannot be updated.");
                    throw new InvalidOperationException("Feedback can only be updated for completed appointments.");
                }
                if ((DateTime.UtcNow - feedback.CreatedAt).TotalHours > 24)
                {
                    _logger.Warn($"Feedback {feedbackId} cannot be updated after 24 hours.");
                    throw new InvalidOperationException("Feedback can only be updated within 24 hours after submission.");
                }

                if (feedbackDto.Rating < 1 || feedbackDto.Rating > 5)
                {
                    _logger.Warn($"Invalid rating: {feedbackDto.Rating}. Rating must be between 1 and 5.");
                    throw new ArgumentException("Rating must be between 1 and 5.");
                }

                feedback.Rating = feedbackDto.Rating;
                feedback.Comments = feedbackDto.Comments ?? feedback.Comments;
                feedback.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.FeedbackRepository.Update(feedback);
                await _unitOfWork.SaveChangesAsync();

                _logger.Info($"User {userId} successfully updated feedback {feedbackId}.");

                return new FeedbackDto
                {
                    AppointmentId = feedback.AppointmentId.GetValueOrDefault(),
                    Rating = feedback.Rating.GetValueOrDefault(),
                    Comments = feedback.Comments
                };
            }
            catch (Exception ex)
            {
                _logger.Error($"An error occurred while updating feedback {feedbackId}: {ex.Message}");
                throw;
            }
        }
    }
}
