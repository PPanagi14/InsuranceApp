using InsuranceApp.Domain.Entities;

namespace InsuranceApp.Application.Common.Interfaces;

public interface IClientRepository : IGenericRepository<Client>
{
    Task<Client?> GetByEmailAsync(string email, CancellationToken ct = default);
    Task<bool> ExistsByEmailAsync(string email, CancellationToken ct = default);
    Task<bool> ExistsByVatNumberAsync(string vatNumber, CancellationToken ct = default);
}

