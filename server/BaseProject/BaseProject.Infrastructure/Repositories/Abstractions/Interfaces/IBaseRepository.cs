using System.Linq.Expressions;

namespace BaseProject.Infrastructure.Repositories.Abstractions.Interfaces;

public interface IBaseRepository<T> where T : class
{
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task DeleteManyAsync(Expression<Func<T, bool>> filter);
    Task<IEnumerable<T>> GetAllAsync();
}