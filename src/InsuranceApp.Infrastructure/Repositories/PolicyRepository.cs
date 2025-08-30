using InsuranceApp.Application.Common.Interfaces;
using InsuranceApp.Domain.Entities;
using InsuranceApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InsuranceApp.Infrastructure.Repositories;

public class PolicyRepository : GenericRepository<Policy>, IPolicyRepository
{
    private readonly AppDbContext db;

    public PolicyRepository(AppDbContext db) : base(db)
    {
        this.db = db;
    }

    public async Task<IReadOnlyList<Policy>> GetActivePoliciesByClientIdAsync(Guid clientId, CancellationToken ct = default) =>
        await db.Policies
                .Where(p => p.ClientId == clientId && p.Status == PolicyStatus.Active)
                .ToListAsync(ct);

    public async Task<IReadOnlyList<Policy>> GetByClientIdAsync(Guid clientId, CancellationToken ct = default)
    {
        return await db.Policies
            .Where(p => p.ClientId == clientId)
            .AsNoTracking()
            .ToListAsync(ct);
    }
}
