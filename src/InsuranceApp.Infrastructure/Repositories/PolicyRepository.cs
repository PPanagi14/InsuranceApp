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
    public async Task<bool> ExistsByPolicyNumberAsync(string policyNumber, string insurer, CancellationToken ct = default) =>
        await db.Policies.AnyAsync(p => p.PolicyNumber == policyNumber && p.Insurer == insurer, ct);

    // 🔹 Queries

    public async Task<List<Policy>> GetExpiringSoonAsync(DateTime cutoffDate, CancellationToken ct = default) =>
        await db.Policies
            .Where(p => p.EndDate <= cutoffDate && p.Status == PolicyStatus.Active)
            .ToListAsync(ct);

    public async Task<Dictionary<Guid, int>> GetPolicyCountsByClientAsync(CancellationToken ct) =>
    await db.Policies
        .GroupBy(p => p.ClientId)
        .Select(g => new { g.Key, Count = g.Count() })
        .ToDictionaryAsync(x => x.Key, x => x.Count, ct);
}
