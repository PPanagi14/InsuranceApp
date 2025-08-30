using BCrypt.Net;
using InsuranceApp.Application.Common.Interfaces;
using InsuranceApp.Application.Features.Users.Commands.LoginUser;
using InsuranceApp.Application.Features.Users.DTOs;
using MediatR;

namespace InsuranceApp.Application.Users.Commands.LoginUser;

public class LoginUserHandler(IUserRepository repo, IUnitOfWork uow, IJwtTokenService jwt)
    : IRequestHandler<LoginUserCommand, AuthResultDto>
{
    public async Task<AuthResultDto> Handle(LoginUserCommand request, CancellationToken ct)
    {
        var user = await repo.GetByUsernameAsync(request.Username, ct);
        if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid credentials");

        var accessToken = jwt.GenerateToken(user);
        var refreshToken = jwt.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        await repo.UpdateAsync(user, ct);
        await uow.SaveChangesAsync(ct);

        return new AuthResultDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddMinutes(30) // matches token expiry
        };
    }
}

