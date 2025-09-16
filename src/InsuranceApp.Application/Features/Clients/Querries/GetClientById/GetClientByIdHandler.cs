using AutoMapper;
using InsuranceApp.Application.Common.Interfaces;
using InsuranceApp.Application.Features.Clients.DTOs;
using InsuranceApp.Domain.Entities;
using MediatR;

namespace InsuranceApp.Application.Features.Clients.Querries.GetClientById;

public class GetClientByIdHandler(IClientRepository clientRepository,IPolicyRepository policyRepository ,IMapper mapper)
    : IRequestHandler<GetClientByIdQuery, ClientDetailDto?>
{
    public async Task<ClientDetailDto?> Handle(GetClientByIdQuery request, CancellationToken ct)
    {
        var client = await clientRepository.GetByIdAsync(request.Id, ct);
        if (client is null)
            throw new KeyNotFoundException($"Client with ID {request.Id} not found");

        var policies = await policyRepository.GetByClientIdAsync(client.Id, ct);

        var dto = mapper.Map<ClientDetailDto>(client);
        dto.Policies = mapper.Map<List<PolicySummaryDto>>(policies);

        dto.TotalPremium = policies.Where(p => p.Status == PolicyStatus.Active)
                           .Sum(p => p.PremiumAmount);
        dto.Status = policies.Any(p => p.Status == PolicyStatus.Active)
            ? "Active"
            : "Inactive";

        return dto;

        return dto;
    }
}
