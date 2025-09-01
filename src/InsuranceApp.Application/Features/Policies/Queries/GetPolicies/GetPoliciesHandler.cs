using AutoMapper;
using InsuranceApp.Application.Common.Interfaces;
using InsuranceApp.Application.Features.Policies.DTOs;
using MediatR;

namespace InsuranceApp.Application.Features.Policies.Queries.GetPolicies;

public class GetPoliciesHandler(IPolicyRepository repo, IMapper mapper)
    : IRequestHandler<GetPoliciesQuery, IReadOnlyList<PolicyDto>>
{
    public async Task<IReadOnlyList<PolicyDto>> Handle(GetPoliciesQuery request, CancellationToken po)
    {
        var policies = await repo.GetAllAsync(po);
        return mapper.Map<IReadOnlyList<PolicyDto>>(policies);
    }
}
