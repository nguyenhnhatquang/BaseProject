namespace BaseProject.API.Extensions;
using Microsoft.EntityFrameworkCore;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        
        //var dbContext = scope.ServiceProvider.GetRequiredService<BaseProjectDbContext>();
        
        //dbContext.Database.Migrate();
    }
}