using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VaccinaCare.Domain;
using VaccinaCare.Domain.Entities;
using VaccinaCare.Repository.Interfaces;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    private readonly IDbContextFactory<VaccinaCareDbContext> _dbContextFactory;
    private readonly ICurrentTime _timeService;
    private readonly IClaimsService _claimsService;

    public GenericRepository(IDbContextFactory<VaccinaCareDbContext> dbContextFactory,
                             ICurrentTime timeService,
                             IClaimsService claimsService)
    {
        _dbContextFactory = dbContextFactory;
        _timeService = timeService;
        _claimsService = claimsService;
    }

    /// <summary>
    /// Thêm một thực thể vào database
    /// </summary>
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await using var context = _dbContextFactory.CreateDbContext();

        entity.CreatedAt = _timeService.GetCurrentTime();
        entity.CreatedBy = _claimsService.GetCurrentUserId;

        var result = await context.Set<TEntity>().AddAsync(entity);
        await context.SaveChangesAsync();

        return result.Entity;
    }

    /// <summary>
    /// Lấy tất cả thực thể với tùy chọn include
    /// </summary>
    public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null,
                                                 params Expression<Func<TEntity, object>>[] includes)
    {
        await using var context = _dbContextFactory.CreateDbContext();
        IQueryable<TEntity> query = context.Set<TEntity>();

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        return await query.ToListAsync();
    }

    /// <summary>
    /// Lấy thực thể theo ID
    /// </summary>
    public async Task<TEntity?> GetByIdAsync(Guid id, params Expression<Func<TEntity, object>>[] includes)
    {
        await using var context = _dbContextFactory.CreateDbContext();
        IQueryable<TEntity> query = context.Set<TEntity>();

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return await query.FirstOrDefaultAsync(x => x.Id == id);
    }

    /// <summary>
    /// Cập nhật thực thể
    /// </summary>
    public async Task<bool> UpdateAsync(TEntity entity)
    {
        await using var context = _dbContextFactory.CreateDbContext();

        entity.UpdatedAt = _timeService.GetCurrentTime();
        entity.UpdatedBy = _claimsService.GetCurrentUserId;

        context.Set<TEntity>().Update(entity);
        await context.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Xóa mềm thực thể
    /// </summary>
    public async Task<bool> SoftRemoveAsync(TEntity entity)
    {
        await using var context = _dbContextFactory.CreateDbContext();

        entity.IsDeleted = true;
        entity.DeletedAt = _timeService.GetCurrentTime();
        entity.DeletedBy = _claimsService.GetCurrentUserId;
        entity.UpdatedAt = _timeService.GetCurrentTime();

        context.Set<TEntity>().Update(entity);
        await context.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Xóa cứng thực thể
    /// </summary>
    public async Task<bool> HardRemoveAsync(TEntity entity)
    {
        await using var context = _dbContextFactory.CreateDbContext();

        context.Set<TEntity>().Remove(entity);
        await context.SaveChangesAsync();

        return true;
    }
}
