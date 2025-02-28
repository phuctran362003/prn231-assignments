using VaccinaCare.Domain.DTOs.FeedBackDTOs;

namespace VaccinaCare.Application.Interface
{
    public interface IFeedbackService
    {
        Task<FeedbackDto> GetFeedbackDetails(Guid id);
        Task<List<FeedbackDto>> GetAllFeedbacksAsync();
    }
}
