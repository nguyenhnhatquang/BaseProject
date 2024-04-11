namespace BaseProject.API.Utils.Database;

using BaseProject.Domain.Entities;
using BaseProject.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using BCrypt.Net;

public static class DbInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        context.Database.EnsureCreated();

        using IDbContextTransaction transaction = context.Database.BeginTransaction();

        try
        {
            if (!context.Roles.Any())
            {
                foreach (AccountPermission accountPemision in Enum.GetValues(typeof(AccountPermission)))
                {
                    context.Roles.Add(new Role
                    {
                        Id = Guid.NewGuid(),
                        Type = accountPemision,
                        Name = accountPemision.ToString()
                    });
                }

                if (!context.Accounts.Any())
                {
                    var accountSuperAdmin = new Account()
                    {
                        Id = Guid.NewGuid(),
                        Username = "super_admin",
                        DisplayName = "Super Admin",
                        Gender = Gender.Other,
                        PasswordHash = BCrypt.HashPassword("NQ_2609"),
                        AcceptTerms = true,
                        IsVerified = true,
                        CreatedOnUtc = DateTime.UtcNow,
                    };

                    context.Accounts.Add(accountSuperAdmin);

                    context.SaveChanges();

                    var rolesSuperAdmin = context.Roles.AsEnumerable();

                    foreach (var role in rolesSuperAdmin)
                    {
                        context.AccountRoles.Add(new AccountRole
                        {
                            Id = Guid.NewGuid(),
                            Account = accountSuperAdmin,
                            Role = role,
                        });
                    }

                    context.SaveChanges();
                }

                context.SaveChanges();
            }

            ;

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
        }
    }
}