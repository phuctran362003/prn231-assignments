using VaccinaCare.Domain.Entities;

namespace VaccinaCare.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<User> UserRepository { get; }
        IGenericRepository<Vaccine> VaccineRepository { get; }
        IGenericRepository<Feedback> FeedbackRepository { get; }

        Task<int> SaveChangesAsync();
    }

}