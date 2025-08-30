using InsuranceApp.Application.Features.Clients.Commands.CreateClient;
using InsuranceApp.Application.Features.Clients.Commands.DeleteClient;
using InsuranceApp.Application.Features.Clients.Commands.RestoreClient;
using InsuranceApp.Application.Features.Clients.Commands.UpdateClient;
using InsuranceApp.Application.Features.Clients.DTOs;
using InsuranceApp.Application.Features.Clients.Querries.GetClientById;
using InsuranceApp.Application.Features.Clients.Querries.GetClients;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateClientCommand command)
    {
        var id = await mediator.Send(command);
        return Ok(id);
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult<IReadOnlyList<ClientDto>>> GetAll()
    {
        var clients = await mediator.Send(new GetClientsQuery());
        return Ok(clients);
    }

    [HttpGet("GetById/{id:guid}")]
    public async Task<ActionResult<ClientDetailDto>> GetById(Guid id)
    {
        var client = await mediator.Send(new GetClientByIdQuery(id));
        if (client is null) return NotFound();
        return Ok(client);
    }

    [HttpPut("Update/{id:guid}")]
    public async Task<ActionResult<ClientDetailDto>> Update(Guid id, [FromBody] UpdateClientCommand command)
    {
        if (id != command.Id)
            return BadRequest("Route ID and command ID must match");

        var updatedClient = await mediator.Send(command);
        return Ok(updatedClient);
    }

    [HttpDelete("Delete/{id:guid}")]
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
