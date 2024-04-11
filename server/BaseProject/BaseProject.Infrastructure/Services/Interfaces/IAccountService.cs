using BaseProject.Domain.Shares;
using BaseProject.DTOs.Account.Responses;

namespace BaseProject.Infrastructure.Services.Interfaces;

public interface IAccountService
{
    Task<Result<AccountResponse>> Login(string username, string password, string ipAddress);
}