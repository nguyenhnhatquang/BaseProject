using BaseProject.Infrastructure.Repositories.Abstractions.Interfaces;

namespace BaseProject.Infrastructure.Interfaces;

public interface IUnitOfWork : IDisposable
{
    #region Repository
    IAccountRepository AccountRepository { get; }
    #endregion
    void CreateTransaction();
    void Commit();
    void Rollback();
    Task CompleteAsync();
    public int ExecuteSqlRaw(string sql, params object[] parameters);
    public IQueryable<TEntity> FromSqlRaw<TEntity>(string sql, params object[] parameters) where TEntity : class;
}