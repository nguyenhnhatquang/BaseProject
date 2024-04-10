using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BaseProject.Infrastructure;

public class ApplicationDbContext : DbContext
{
    private readonly IConfiguration? _configuration;

    public ApplicationDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) :
        base(options)
    {
        _configuration = configuration;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(_configuration?.GetConnectionString("PostgreSql"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}