using BaseProject.Infrastructure.Interfaces;
using BaseProject.Infrastructure.Repositories.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace BaseProject.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    #region Properties

    private bool _disposed = false;
    private IDbContextTransaction? _objTran;
    private readonly ApplicationDbContext _dbContext;

    #endregion

    #region Repositories

    private AccountRepository? _accountRepository;
    public IAccountRepository AccountRepository => _accountRepository ??= new AccountRepository(_dbContext);

    #endregion

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }


    public void CreateTransaction()
    {
        _objTran = _dbContext.Database.BeginTransaction();
    }

    public void Commit()
    {
        _objTran?.Commit();
    }

    public void Rollback()
    {
        _objTran?.Rollback();
        _dbContext?.Dispose();
    }

    public async Task CompleteAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public int ExecuteSqlRaw(string sql, params object[] parameters)
    {
        return _dbContext.Database.ExecuteSqlRaw(sql, parameters);
    }

    public IQueryable<TEntity> FromSqlRaw<TEntity>(string sql, params object[] parameters) where TEntity : class
    {
        return _dbContext.Set<TEntity>().FromSqlRaw(sql, parameters);
    }

    public void Dispose()
    {
        Dispose(true);

        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
        {
            _objTran?.Dispose();
            _dbContext?.Dispose();
        }

        _disposed = true;
    }
}