using VaccinaCare.Domain.DTOs.FeedBackDTOs;

namespace VaccinaCare.Application.Interface
{
    public interface IFeedbackService
    {
        Task<FeedbackDto> CreateFeedbackAsync(FeedbackDto feedbackDto);
        Task DeleteFeedbackAsync(Guid feedbackId);
        Task<List<FeedbackDto>> GetAllFeedbacksAsync();
        Task<FeedbackDto> GetFeedbackByIdAsync(Guid feedbackId);
        Task<FeedbackDto> UpdateFeedbackAsync(Guid feedbackId, FeedbackDto feedbackDto);
    }
}
