using InsuranceApp.Application.Common.Interfaces;
using InsuranceApp.Application.Features.Users.DTOs;
using MediatR;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace InsuranceApp.Application.Features.Users.Commands.RefreshToken;

public class RefreshTokenHandler(IUserRepository repo, IUnitOfWork uow, IJwtTokenService jwt)
    : IRequestHandler<RefreshTokenCommand, AuthResultDto>
{
    public async Task<AuthResultDto> Handle(RefreshTokenCommand request, CancellationToken ct)
    {
        var principal = new JwtSecurityTokenHandler().ValidateToken(
            request.AccessToken,
            new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false, // allow expired
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes("mysuperlongsecretkeythatisharder2guess!!"))
            },
            out var _);

        var username = principal.Identity?.Name;
        var user = await repo.GetByUsernameAsync(username!, ct);

        if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            throw new SecurityTokenException("Invalid refresh token");

        var newAccessToken = jwt.GenerateToken(user);
        var newRefreshToken = jwt.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        await repo.UpdateAsync(user, ct);
        await uow.SaveChangesAsync(ct);

        return new AuthResultDto
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken,
            ExpiresAt = DateTime.UtcNow.AddMinutes(30)
        };
    }
}
