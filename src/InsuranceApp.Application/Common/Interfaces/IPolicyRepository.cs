using InsuranceApp.Domain.Entities;

namespace InsuranceApp.Application.Common.Interfaces;

public interface IPolicyRepository : IGenericRepository<Policy>
{
    Task<IReadOnlyList<Policy>> GetActivePoliciesByClientIdAsync(Guid clientId, CancellationToken ct = default);
    Task<IReadOnlyList<Policy>> GetByClientIdAsync(Guid clientId, CancellationToken ct = default);
} 
