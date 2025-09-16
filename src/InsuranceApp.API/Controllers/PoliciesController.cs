using InsuranceApp.Application.Features.Clients.DTOs;
using InsuranceApp.Application.Features.Clients.Querries.GetClients;
using InsuranceApp.Application.Features.Policies.Commands.CreatePolicy;
using InsuranceApp.Application.Features.Policies.Commands.DeletePolicy;
using InsuranceApp.Application.Features.Policies.Commands.RestorePolicy;
using InsuranceApp.Application.Features.Policies.Commands.UpdatePolicy;
using InsuranceApp.Application.Features.Policies.DTOs;
using InsuranceApp.Application.Features.Policies.Queries.GetAllPoliciesWithDetails;
using InsuranceApp.Application.Features.Policies.Queries.GetPolicies;
using InsuranceApp.Application.Features.Policies.Queries.GetPoliciesByClientId;
using InsuranceApp.Application.Features.Policies.Queries.GetPolicyById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceApp.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PoliciesController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<PolicyDetailDto>> Create([FromBody] CreatePolicyCommand command)
    {
        var policy = await mediator.Send(command);
        return Ok(policy);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<PolicyDto>>> GetAll()
    {
        var policies = await mediator.Send(new GetPoliciesQuery());
        return Ok(policies);
    }
    [HttpGet("GetAllPoliciesWithDetails")]
    public async Task<ActionResult<IReadOnlyList<PolicyDetailDto>>> GetAllPoliciesWithDetails()
    {
        var policies = await mediator.Send(new GetAllPoliciesWithDetailsQuery());
        return Ok(policies);
    }

    [HttpGet("by-client/{clientId:guid}")]
    public async Task<ActionResult<IReadOnlyList<PolicyDto>>> GetByClientId(Guid clientId)
    {
        var policies = await mediator.Send(new GetPoliciesByClientIdQuery(clientId));
        return Ok(policies);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PolicyDetailDto>> GetById(Guid id)
    {
        var policy = await mediator.Send(new GetPolicyByIdQuery(id));
        if (policy is null) return NotFound();
        return Ok(policy);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<PolicyDetailDto>> Update(Guid id, [FromBody] UpdatePolicyCommand command)
    {
        var updatedCommand = command with { Id = id }; //  use 'with' expression to set init-only property
        var updatedPolicy = await mediator.Send(updatedCommand);
        return Ok(updatedPolicy);
    }


    [HttpDelete("{id:guid}")]
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
