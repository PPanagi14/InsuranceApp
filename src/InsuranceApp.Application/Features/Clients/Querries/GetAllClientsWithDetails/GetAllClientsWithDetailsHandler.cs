using AutoMapper;
using InsuranceApp.Application.Common.Interfaces;
using InsuranceApp.Application.Features.Clients.DTOs;
using InsuranceApp.Domain.Entities;
using MediatR;

namespace InsuranceApp.Application.Features.Clients.Querries.GetAllClientsWithDetails;

public class GetAllClientsWithDetailsHandler(
    IClientRepository clientRepo,
    IPolicyRepository policyRepo,
    IMapper mapper)
    : IRequestHandler<GetAllClientsWithDetailsQuery, IReadOnlyList<ClientDetailDto>>
{
    public async Task<IReadOnlyList<ClientDetailDto>> Handle(GetAllClientsWithDetailsQuery request, CancellationToken ct)
    {
        var clients = await clientRepo.GetAllAsync(ct);

        var result = new List<ClientDetailDto>();

        foreach (var client in clients)
        {
            var policies = await policyRepo.GetByClientIdAsync(client.Id, ct);

            var dto = mapper.Map<ClientDetailDto>(client);
            dto.Policies = mapper.Map<List<PolicySummaryDto>>(policies);

            dto.PoliciesCount = policies.Count;
            dto.TotalPremium = policies
                .Where(p => p.Status == PolicyStatus.Active)
                .Sum(p => p.PremiumAmount);

            dto.Status = policies.Any(p => p.Status == PolicyStatus.Active)
                ? "Active"
                : "Inactive";

            result.Add(dto);
        }

        return result;
    }
}
