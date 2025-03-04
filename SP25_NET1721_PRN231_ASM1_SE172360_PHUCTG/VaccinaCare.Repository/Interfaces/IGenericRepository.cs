using System.Linq.Expressions;
using VaccinaCare.Domain.Entities;

namespace VaccinaCare.Repository.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null,
                                                         params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity?> GetByIdAsync(Guid id, params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity> AddAsync(TEntity entity);

        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> SoftRemoveAsync(TEntity entity);
        Task<bool> HardRemoveAsync(TEntity entity);
    }
}