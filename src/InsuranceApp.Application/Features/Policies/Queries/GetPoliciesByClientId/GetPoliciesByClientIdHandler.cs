using AutoMapper;
using InsuranceApp.Application.Common.Interfaces;
using InsuranceApp.Application.Features.Policies.DTOs;
using MediatR;

namespace InsuranceApp.Application.Features.Policies.Queries.GetPoliciesByClientId;

public class GetPoliciesByClientIdHandler(IPolicyRepository repo, IMapper mapper)
    : IRequestHandler<GetPoliciesByClientIdQuery, IReadOnlyList<PolicyDto>>
{
    public async Task<IReadOnlyList<PolicyDto>> Handle(GetPoliciesByClientIdQuery request, CancellationToken ct)
    {
        var policies = await repo.GetByClientIdAsync(request.ClientId, ct);
        return mapper.Map<IReadOnlyList<PolicyDto>>(policies);
    }
}
