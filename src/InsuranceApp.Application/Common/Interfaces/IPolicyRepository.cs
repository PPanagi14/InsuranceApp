using InsuranceApp.Domain.Entities;

namespace InsuranceApp.Application.Common.Interfaces;

public interface IPolicyRepository : IGenericRepository<Policy>
{
    Task<IReadOnlyList<Policy>> GetActivePoliciesByClientIdAsync(Guid clientId, CancellationToken ct = default);
    Task<IReadOnlyList<Policy>> GetByClientIdAsync(Guid clientId, CancellationToken ct = default);
    Task<bool> ExistsByPolicyNumberAsync(string policyNumber, string insurer, CancellationToken ct = default);

    // 🔹 Useful queries
    Task<List<Policy>> GetExpiringSoonAsync(DateTime cutoffDate, CancellationToken ct = default);
    Task<Dictionary<Guid, int>> GetPolicyCountsByClientAsync(CancellationToken ct);

}
