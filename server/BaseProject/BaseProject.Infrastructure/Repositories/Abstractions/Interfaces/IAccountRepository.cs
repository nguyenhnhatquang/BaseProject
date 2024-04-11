using BaseProject.Domain.Entities;

namespace BaseProject.Infrastructure.Repositories.Abstractions.Interfaces;

public interface IAccountRepository : IBaseRepository<Account>
{
    Task<Account?> GetById(Guid id);
    Task<Account?> Login(string username, string password);
}