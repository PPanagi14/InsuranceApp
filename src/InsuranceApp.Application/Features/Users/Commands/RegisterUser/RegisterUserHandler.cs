using InsuranceApp.Application.Common.Interfaces;
using InsuranceApp.Domain.Entities;
using MediatR;
using BCrypt.Net;

namespace InsuranceApp.Application.Features.Users.Commands.RegisterUser;

public class RegisterUserHandler(IUserRepository repo, IUnitOfWork uow)
    : IRequestHandler<RegisterUserCommand, Guid>
{
    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken ct)
    {
        var hashed = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var user = new User
        {
            Username = request.Username,
            PasswordHash = hashed,
            Role = request.Role
        };

        await repo.AddAsync(user, ct);
        await uow.SaveChangesAsync(ct);

        return user.Id;
    }
}
