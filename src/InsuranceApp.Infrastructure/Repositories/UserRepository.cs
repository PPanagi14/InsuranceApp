using InsuranceApp.Application.Common.Interfaces;
using InsuranceApp.Domain.Entities;
using InsuranceApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InsuranceApp.Infrastructure.Repositories;

public class UserRepository(AppDbContext db) : GenericRepository<User>(db), IUserRepository
{
    public async Task<User?> GetByUsernameAsync(string username, CancellationToken ct)
    {
        return await db.Users
            .Include(u => u.Roles) //  load roles
            .ThenInclude(r => r.RoleType) //  load role type
            .FirstOrDefaultAsync(u => u.Username == username, ct);
    }
}
