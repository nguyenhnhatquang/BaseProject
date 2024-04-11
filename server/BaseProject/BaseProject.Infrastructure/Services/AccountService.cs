namespace BaseProject.Infrastructure.Services;

using BaseProject.Domain.Entities;
using BaseProject.Domain.Shares;
using BaseProject.DTOs.Account;
using BaseProject.DTOs.Account.Responses;
using BaseProject.Infrastructure.Authorization.Interfaces;
using BaseProject.Infrastructure.Interfaces;
using BaseProject.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Options;
using BCrypt.Net;

public class AccountService : IAccountService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtUtils _jwtUtils;
    private readonly AppSettings _appSettings;

    public AccountService(IUnitOfWork unitOfWork, IJwtUtils jwtUtils, IOptions<AppSettings> appSettings)
    {
        _unitOfWork = unitOfWork;
        _jwtUtils = jwtUtils;
        _appSettings = appSettings.Value;
    }

    public async Task<Result<AccountResponse>> Login(string username, string password, string ipAddress)
    {
        var account = await _unitOfWork.AccountRepository.Login(username, password);

        if (account is null || !BCrypt.Verify(password, account.PasswordHash))
        {
            return Result.Failure<AccountResponse>(AccountErrors.InCorrect);
        }

        var jwtToken = _jwtUtils.GenerateJwtToken(account);
        var refreshToken = _jwtUtils.GenerateRefreshToken(ipAddress);
        account.RefreshTokens ??= [];

        if (account.RefreshTokens.Any(x => x.CreatedByIp == ipAddress))
        {
            refreshToken = account.RefreshTokens.First(x => x.CreatedByIp.Equals(ipAddress));
        }
        else
        {
            account.RefreshTokens.Add(refreshToken);
        }

        RemoveOldRefreshTokens(account);

        await _unitOfWork.AccountRepository.UpdateAsync(account);
        await _unitOfWork.CompleteAsync();

        var response = new AccountResponse
        {
            Id = account.Id,
            Username = account.Username,
            DisplayName = account.DisplayName,
            Email = account.Email,
            Avatar = account.Avatar,
            Gender = account.Gender,
            PhoneNumber = account.PhoneNumber,
            Birthday = account.Birthday,
            JwtToken = jwtToken,
            Roles = account.AccountRoles.Select(x => x.Role).ToList(),
            RefreshToken = refreshToken.Token,
        };

        return Result.Success(response);
    }

    private void RemoveOldRefreshTokens(Account account)
    {
        account.RefreshTokens.RemoveAll(x =>
            !x.IsActive && x.CreatedOnUtc.AddDays(_appSettings.RefreshTokenTTL) <= DateTime.UtcNow);
    }
}