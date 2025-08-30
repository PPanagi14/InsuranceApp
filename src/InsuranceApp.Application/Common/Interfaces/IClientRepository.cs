using InsuranceApp.Domain.Entities;

namespace InsuranceApp.Application.Common.Interfaces;

public interface IClientRepository : IGenericRepository<Client>
{
    Task<Client?> GetByEmailAsync(string email, CancellationToken ct = default);
}

