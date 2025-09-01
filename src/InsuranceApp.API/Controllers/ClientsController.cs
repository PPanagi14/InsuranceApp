using InsuranceApp.Application.Features.Clients.Commands.CreateClient;
using InsuranceApp.Application.Features.Clients.Commands.DeleteClient;
using InsuranceApp.Application.Features.Clients.Commands.RestoreClient;
using InsuranceApp.Application.Features.Clients.Commands.UpdateClient;
using InsuranceApp.Application.Features.Clients.DTOs;
using InsuranceApp.Application.Features.Clients.Querries.GetClientById;
using InsuranceApp.Application.Features.Clients.Querries.GetClients;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceApp.API.Controllers;


[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ClientsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateClientCommand command)
    {
        var id = await mediator.Send(command);
        return Ok(id);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ClientDto>>> GetAll()
    {
        var clients = await mediator.Send(new GetClientsQuery());
        return Ok(clients);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ClientDetailDto>> GetById(Guid id)
    {
        var client = await mediator.Send(new GetClientByIdQuery(id));
        if (client is null) return NotFound();
        return Ok(client);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ClientDetailDto>> Update(Guid id, [FromBody] UpdateClientCommand command)
    {
        var updatedCommand = command with { Id = id }; //  use 'with' expression to set init-only property
        var updatedClient = await mediator.Send(updatedCommand);
        return Ok(updatedClient);
    }


    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await mediator.Send(new DeleteClientCommand(id));
        if (!success)
            return NotFound();

        return NoContent();
    }

    [HttpPost("{id:guid}/restore")]
    public async Task<IActionResult> Restore(Guid id)
    {
        var success = await mediator.Send(new RestoreClientCommand(id));
        if (!success)
            return NotFound();

        return Ok();
    }
}
