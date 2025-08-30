using InsuranceApp.Application.Features.Users.Commands.LoginUser;
using InsuranceApp.Application.Features.Users.Commands.RefreshToken;
using InsuranceApp.Application.Features.Users.Commands.RegisterUser;
using InsuranceApp.Application.Features.Users.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<Guid>> Register(RegisterUserCommand command)
    {
        var id = await mediator.Send(command);
        return Ok(id);
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(LoginUserCommand command)
    {
        var token = await mediator.Send(command);
        return Ok(new { Token = token });
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<AuthResultDto>> Refresh(RefreshTokenCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }
}
