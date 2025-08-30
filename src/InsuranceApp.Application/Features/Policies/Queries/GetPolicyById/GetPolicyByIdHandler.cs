using AutoMapper;
using InsuranceApp.Application.Common.Interfaces;
using InsuranceApp.Application.Features.Policies.DTOs;
using MediatR;

namespace InsuranceApp.Application.Features.Policies.Queries.GetPolicyById;

public class GetPolicyByIdHandler(IPolicyRepository repo, IMapper mapper)
    : IRequestHandler<GetPolicyByIdQuery, PolicyDetailDto?>
{
    public async Task<PolicyDetailDto?> Handle(GetPolicyByIdQuery request, CancellationToken ct)
    {
        var policy = await repo.GetByIdAsync(request.Id, ct);
        return policy is null ? null : mapper.Map<PolicyDetailDto>(policy);
    }
}
