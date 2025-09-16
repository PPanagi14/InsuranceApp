using InsuranceApp.Application.Common.Interfaces;
using InsuranceApp.Domain.Entities;
using InsuranceApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InsuranceApp.Infrastructure.Repositories;

public class ClientRepository : GenericRepository<Client>, IClientRepository
{
    private readonly AppDbContext db;

    public ClientRepository(AppDbContext db) : base(db)
    {
        this.db = db;
    }

    public async Task<Client?> GetByEmailAsync(string email, CancellationToken ct = default) =>
        await db.Clients.FirstOrDefaultAsync(c => c.Email == email, ct);
    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken ct = default) =>
        await db.Clients.AnyAsync(c => c.Email == email, ct);

    public async Task<bool> ExistsByVatNumberAsync(string vatNumber, CancellationToken ct = default) =>
        await db.Clients.AnyAsync(c => c.VatNumber == vatNumber, ct);
}
