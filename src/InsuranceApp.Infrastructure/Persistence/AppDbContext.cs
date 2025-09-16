using InsuranceApp.Application.Common.Interfaces;
using InsuranceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InsuranceApp.Infrastructure.Persistence;

public class AppDbContext(
    DbContextOptions<AppDbContext> options,
    ICurrentUserService currentUserService,
    ILogger<AppDbContext> logger)
    : DbContext(options)
{
    private readonly ICurrentUserService _currentUserService = currentUserService;
    private readonly ILogger<AppDbContext> _logger = logger;

    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Policy> Policies => Set<Policy>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<RoleTypeEntity> RoleTypes => Set<RoleTypeEntity>();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;
        var currentUserId = _currentUserService.UserId;

        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAtUtc = now;
                entry.Entity.UpdatedAtUtc = now;
                entry.Entity.CreatedBy = currentUserId;

                _logger.LogInformation(
                    "Entity {Entity} with ID {Id} was created by {UserId} at {Time}",
                    entry.Entity.GetType().Name,
                    entry.Entity.Id,
                    currentUserId,
                    now);
            }

            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAtUtc = now;
                entry.Entity.UpdatedBy = currentUserId;

                _logger.LogInformation(
                    "Entity {Entity} with ID {Id} was updated by {UserId} at {Time}",
                    entry.Entity.GetType().Name,
                    entry.Entity.Id,
                    currentUserId,
                    now);
            }

            else if (entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Modified;
                entry.Entity.DeletedAtUtc = now;
                entry.Entity.DeletedBy = currentUserId;
                entry.Entity.UpdatedAtUtc = now;

                _logger.LogWarning(
                    "Entity {Entity} with ID {Id} was soft deleted by {UserId} at {Time}",
                    entry.Entity.GetType().Name,
                    entry.Entity.Id,
                    currentUserId,
                    now);
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        // Apply query filters
        modelBuilder.Entity<Client>().HasQueryFilter(e => e.DeletedAtUtc == null);
        modelBuilder.Entity<Policy>().HasQueryFilter(e => e.DeletedAtUtc == null);
        modelBuilder.Entity<User>().HasQueryFilter(e => e.DeletedAtUtc == null);
        modelBuilder.Entity<Role>().HasQueryFilter(e => e.DeletedAtUtc == null);
        modelBuilder.Entity<RoleTypeEntity>().HasQueryFilter(e => e.DeletedAtUtc == null);

        // Force all DateTime to UTC
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties()
                 .Where(p => p.ClrType == typeof(DateTime) || p.ClrType == typeof(DateTime?)))
            {
                property.SetValueConverter(new UtcValueConverter());
            }
        }
    }

}
