using Microsoft.EntityFrameworkCore;

namespace InsuranceApp.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    // We'll add DbSets later, e.g.:
    // public DbSet<Client> Clients => Set<Client>();
}
