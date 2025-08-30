using InsuranceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InsuranceApp.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Policy> Policies => Set<Policy>();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAtUtc = now;
                    entry.Entity.UpdatedAtUtc = now;
                    break;

                case EntityState.Modified:
                    entry.Entity.UpdatedAtUtc = now;
                    break;

                case EntityState.Deleted:
                    // Soft delete → mark as deleted instead of removing
                    entry.State = EntityState.Modified;
                    entry.Entity.DeletedAtUtc = now;
                    entry.Entity.UpdatedAtUtc = now;
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        // Global query filter → exclude soft-deleted entities automatically
        modelBuilder.Entity<Client>().HasQueryFilter(e => e.DeletedAtUtc == null);
        modelBuilder.Entity<Policy>().HasQueryFilter(e => e.DeletedAtUtc == null);

        base.OnModelCreating(modelBuilder);
    }
}
