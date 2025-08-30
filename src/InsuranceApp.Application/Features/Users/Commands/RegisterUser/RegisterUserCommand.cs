using MediatR;

namespace InsuranceApp.Application.Features.Users.Commands.RegisterUser;

public record RegisterUserCommand(string Username, string Password, string RoleCode) : IRequest<Guid>;

