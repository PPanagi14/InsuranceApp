using InsuranceApp.Domain.Entities;

namespace InsuranceApp.Application.Common.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetByUsernameAsync(string username, CancellationToken ct);
}
