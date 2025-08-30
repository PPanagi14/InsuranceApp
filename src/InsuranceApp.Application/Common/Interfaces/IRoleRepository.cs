using InsuranceApp.Domain.Entities;

namespace InsuranceApp.Application.Common.Interfaces;

public interface IRoleRepository : IGenericRepository<Role>
{
    Task<Role?> GetByCodeAsync(string code, CancellationToken ct);
}
