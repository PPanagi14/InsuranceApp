using InsuranceApp.Application.Features.Policies.Commands.CreatePolicy;
using InsuranceApp.Application.Features.Policies.Commands.DeletePolicy;
using InsuranceApp.Application.Features.Policies.Commands.RestorePolicy;
using InsuranceApp.Application.Features.Policies.Commands.UpdatePolicy;
using InsuranceApp.Application.Features.Policies.DTOs;
using InsuranceApp.Application.Features.Policies.Queries.GetPoliciesByClientId;
using InsuranceApp.Application.Features.Policies.Queries.GetPolicyById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PoliciesController(IMediator mediator) : ControllerBase
{
    [HttpPost("Create")]
    public async Task<ActionResult<PolicyDetailDto>> Create([FromBody] CreatePolicyCommand command)
    {
        var policy = await mediator.Send(command);
        return Ok(policy);
    }

    [HttpGet("GetByClientId/{clientId:guid}")]
    public async Task<ActionResult<IReadOnlyList<PolicyDto>>> GetByClientId(Guid clientId)
    {
        var policies = await mediator.Send(new GetPoliciesByClientIdQuery(clientId));
        return Ok(policies);
    }

    [HttpGet("GetById/{id:guid}")]
    public async Task<ActionResult<PolicyDetailDto>> GetById(Guid id)
    {
        var policy = await mediator.Send(new GetPolicyByIdQuery(id));
        if (policy is null) return NotFound();
        return Ok(policy);
    }

    [HttpPut("Update/{id:guid}")]
    public async Task<ActionResult<PolicyDetailDto>> Update(Guid id, [FromBody] UpdatePolicyCommand command)
    {
        if (id != command.Id)
            return BadRequest("Route ID and command ID must match");

        var updated = await mediator.Send(command);
        return Ok(updated);
    }

    [HttpDelete("Delete/{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await mediator.Send(new DeletePolicyCommand(id));
        if (!success)
            return NotFound();

        return NoContent();
    }

    [HttpPost("{id:guid}/restore")]
    public async Task<IActionResult> Restore(Guid id)
    {
        var success = await mediator.Send(new RestorePolicyCommand(id));
        if (!success)
            return NotFound();

        return Ok();
    }
}
