using System.Linq.Expressions;
using BaseProject.Infrastructure.Repositories.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Infrastructure.Repositories.Abstractions;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly DbContext _dbContext;
    protected readonly DbSet<T> _dbSet;

    protected BaseRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
        
        _dbSet = dbContext.Set<T>();
    }
    
    public async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        
        return entity;
    }

    public Task UpdateAsync(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        
        return Task.CompletedTask;
    }

    public Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        
        return Task.CompletedTask;
    }

    public Task DeleteManyAsync(Expression<Func<T, bool>> filter)
    {
        var entities = _dbSet.Where(filter);

        _dbSet.RemoveRange(entities);

        return Task.CompletedTask;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }
}