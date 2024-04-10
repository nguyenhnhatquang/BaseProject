using BaseProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BaseProject.Infrastructure;

public class ApplicationDbContext : DbContext
{
    private readonly IConfiguration? _configuration;

    public ApplicationDbContext()
    {

    }
    
    public ApplicationDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public virtual DbSet<Account> Accounts { get; init; } = null!;
    public virtual DbSet<AccountRole> AccountRoles { get; init; } = null!;
    public virtual DbSet<Role> Roles { get; init; } = null!;
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) :
        base(options)
    {
        _configuration = configuration;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(_configuration?.GetConnectionString("PostgreSql") ?? "Server=aws-0-ap-southeast-1.pooler.supabase.com;Port=5432;CommandTimeout=0;User Id=postgres.jvmbtsldxletrvloeouc;Password=IyScum8HLxZ5;Database=postgres;");
            //optionsBuilder.UseNpgsql(_configuration?.GetConnectionString("PostgreSql"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("public");
        // Table Account
        modelBuilder.Entity<Account>(entity =>
        {
            entity.Property(x => x.Id)
                  .HasDefaultValueSql("gen_random_uuid()")
                  .ValueGeneratedOnAdd();

            entity.Property(x => x.Gender)
                  .IsRequired()
                  .HasDefaultValue(Gender.Other);

            entity.Property(x => x.PasswordHash)
                  .IsRequired();

            entity.Property(x => x.Username)
                 .IsRequired();

            entity.Property(e => e.AcceptTerms)
                  .IsRequired()
                  .HasDefaultValue(false);

            entity.Property(e => e.IsVerified)
                  .IsRequired()
                  .HasDefaultValue(false);

            entity.Property(e => e.IsDeleted)
                  .IsRequired()
                  .HasDefaultValue(false);

            entity.Property(e => e.Birthday)
                  .HasColumnType("DATE");

            entity.Property(e => e.CreatedOnUtc)
                  .HasDefaultValueSql("now() at time zone('utc')")
                  .ValueGeneratedOnAdd();
        });
        modelBuilder.Entity<Account>().HasQueryFilter(e => !e.IsDeleted);
        // Table Role
        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(x => x.Id)
                  .HasDefaultValueSql("gen_random_uuid()")
                  .ValueGeneratedOnAdd();

            entity.Property(x => x.Type)
                  .HasColumnType("numeric(2, 0)");
        });
        // Table AccountRole
        modelBuilder.Entity<AccountRole>(entity =>
        {
            entity.Property(x => x.Id)
                  .HasDefaultValueSql("gen_random_uuid()")
                  .ValueGeneratedOnAdd();
        });
        // Table Refresh Token
        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.Property(x => x.Id)
                  .HasDefaultValueSql("gen_random_uuid()")
                  .ValueGeneratedOnAdd();
        });
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}