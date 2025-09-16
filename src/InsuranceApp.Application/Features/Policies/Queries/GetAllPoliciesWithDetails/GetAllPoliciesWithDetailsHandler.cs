using AutoMapper;
using InsuranceApp.Application.Common.Interfaces;
using InsuranceApp.Application.Features.Policies.DTOs;
using InsuranceApp.Application.Features.Policies.Queries.GetPolicies;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApp.Application.Features.Policies.Queries.GetAllPoliciesWithDetails;

public class GetAllPoliciesWithDetailsHandler(IPolicyRepository repo, IMapper mapper)
    : IRequestHandler<GetAllPoliciesWithDetailsQuery, IReadOnlyList<PolicyDetailDto>>
{
    public async Task<IReadOnlyList<PolicyDetailDto>> Handle(GetAllPoliciesWithDetailsQuery request, CancellationToken po)
    {
        var policies = await repo.GetAllAsync(po);
        return mapper.Map<IReadOnlyList<PolicyDetailDto>>(policies);
    }
}
