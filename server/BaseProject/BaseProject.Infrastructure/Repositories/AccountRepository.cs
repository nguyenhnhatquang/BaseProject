using BaseProject.Domain.Entities;
using BaseProject.Infrastructure.Repositories.Abstractions;
using BaseProject.Infrastructure.Repositories.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Infrastructure.Repositories;

public class AccountRepository : BaseRepository<Account>, IAccountRepository
{
    public AccountRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Account?> GetById(Guid id)
    {
        var account = await _dbSet
            .Where(a => a.Id == id)
            .Include(x => x.AccountRoles)
            .ThenInclude(x => x.Role)
            .FirstOrDefaultAsync();

        return account;
    }

    public async Task<Account?> Login(string username, string password)
    {
        var account = await _dbSet
            .Where(e => e.Username == username)
            .Include(x => x.RefreshTokens)
            .Include(x => x.AccountRoles)
            .ThenInclude(x => x.Role)
            .FirstOrDefaultAsync();

        return account;
    }
}