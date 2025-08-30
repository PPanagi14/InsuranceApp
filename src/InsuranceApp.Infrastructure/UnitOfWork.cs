using InsuranceApp.Application.Common.Interfaces;
using InsuranceApp.Infrastructure.Persistence;

namespace InsuranceApp.Infrastructure;

public class UnitOfWork(AppDbContext db) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken ct = default) =>
        db.SaveChangesAsync(ct);
}
