using InsuranceApp.Application.Common.Interfaces;
using InsuranceApp.Domain.Entities;
using InsuranceApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InsuranceApp.Infrastructure.Repositories;

public class UserRepository(AppDbContext db) : GenericRepository<User>(db), IUserRepository
{
    public async Task<User?> GetByUsernameAsync(string username, CancellationToken ct = default)
    {
        return await db.Users.FirstOrDefaultAsync(u => u.Username == username, ct);
    }
}
