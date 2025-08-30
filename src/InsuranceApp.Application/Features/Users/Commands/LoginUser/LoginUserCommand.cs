using InsuranceApp.Application.Features.Users.DTOs;
using MediatR;

namespace InsuranceApp.Application.Features.Users.Commands.LoginUser;


public record LoginUserCommand(string Username, string Password) : IRequest<AuthResultDto>;