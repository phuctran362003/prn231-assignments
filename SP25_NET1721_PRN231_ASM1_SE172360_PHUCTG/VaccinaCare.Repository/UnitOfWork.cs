using VaccinaCare.Domain;
using VaccinaCare.Domain.Entities;
using VaccinaCare.Repository.Interfaces;

namespace VaccinaCare.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VaccinaCareDbContext _dbContext;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Vaccine> _vaccineRepository;
        private readonly IGenericRepository<Feedback> _feedbackRepository;

        public UnitOfWork(VaccinaCareDbContext dbContext, IGenericRepository<User> userRepository, IGenericRepository<Vaccine> vaccineRepository, IGenericRepository<Feedback> feedbackRepository)
        {
            _dbContext = dbContext;
            _userRepository = userRepository;
            _vaccineRepository = vaccineRepository;
            _feedbackRepository = feedbackRepository;
        }

        public IGenericRepository<User> UserRepository => _userRepository;
        public IGenericRepository<Vaccine> VaccineRepository => _vaccineRepository;
        public IGenericRepository<Feedback> FeedbackRepository => _feedbackRepository;

        public Task<int> SaveChangesAsync()
        {
            try
            {
                return _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}