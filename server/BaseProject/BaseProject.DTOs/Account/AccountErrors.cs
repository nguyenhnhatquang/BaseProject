using BaseProject.Domain.Shares;

namespace BaseProject.DTOs.Account;

public static class AccountErrors
{
    public static readonly Error NotFound = new Error("Account.NotFound", "Tài khoản không tồn tại.");
    public static readonly Error InCorrect = new Error("Account.InCorrect", "Tài khoản hoặc mật khẩu không chính xác.");
}