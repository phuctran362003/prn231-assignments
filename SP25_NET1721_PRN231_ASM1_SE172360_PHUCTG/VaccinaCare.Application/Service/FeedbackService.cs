using VaccinaCare.Application.Interface;
using VaccinaCare.Application.Interface.Common;
using VaccinaCare.Domain.DTOs.FeedBackDTOs;
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

        public async Task<List<FeedbackDto>> GetAllFeedbacksAsync()
        {
            try
            {
                // Include FeedbackType in the query
                var feedbacks = await _unitOfWork.FeedbackRepository.GetAllAsync(
                    includes: f => f.FeedbackType
                );

                var feedbackDtos = feedbacks.Select(feedback => new FeedbackDto
                {
                    Rating = feedback.Rating.GetValueOrDefault(),
                    Comments = feedback.Comments,
                    FeedbackTypeName = feedback.FeedbackType?.Name // Get FeedbackType Name
                }).ToList();

                return feedbackDtos;
            }
            catch (Exception ex)
            {
                _logger.Error($"Error while fetching feedbacks: {ex.Message}");
                throw new Exception("An error occurred while fetching feedbacks. Please try again later.");
            }
        }


        public async Task<FeedbackDto> GetFeedbackDetails(Guid id)
        {
            try
            {
                // Include FeedbackType in the query
                var feedback = await _unitOfWork.FeedbackRepository.GetByIdAsync(id, f => f.FeedbackType);

                if (feedback == null)
                {
                    throw new Exception("Feedback not found");
                }

                var feedbackDto = new FeedbackDto
                {
                    Rating = feedback.Rating.GetValueOrDefault(),
                    Comments = feedback.Comments,
                    FeedbackTypeName = feedback.FeedbackType?.Name // Include FeedbackType Name
                };

                return feedbackDto;
            }
            catch (Exception ex)
            {
                _logger.Error($"Error while fetching feedback details: {ex.Message}");
                throw new Exception("An error occurred while fetching feedback details. Please try again later.");
            }
        }





    }
}
