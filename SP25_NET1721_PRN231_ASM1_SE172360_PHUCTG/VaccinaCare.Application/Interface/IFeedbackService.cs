using VaccinaCare.Domain.DTOs.FeedBackDTOs;

namespace VaccinaCare.Application.Interface
{
    public interface IFeedbackService
    {
        Task<List<FeedbackDto>> GetAllFeedbacksAsync();
    }
}
