using InsuranceApp.Application.Common.Interfaces;
using InsuranceApp.Domain.Entities;
using InsuranceApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InsuranceApp.Infrastructure.Repositories;

public class RoleRepository(AppDbContext db)
    : GenericRepository<Role>(db), IRoleRepository
{
    public async Task<Role?> GetByCodeAsync(string code, CancellationToken ct)
    {
        return await db.Roles
            .Include(r => r.RoleType)
            .FirstOrDefaultAsync(r => r.RoleType.Code == code, ct);
    }
}
