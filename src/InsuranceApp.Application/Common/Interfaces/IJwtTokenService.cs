using InsuranceApp.Domain.Entities;

namespace InsuranceApp.Application.Common.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(User user);
    string GenerateRefreshToken();
}
