using AutoMapper;
using InsuranceApp.Application.Common.Interfaces;
using InsuranceApp.Application.Features.Clients.DTOs;
using InsuranceApp.Domain.Entities;
using MediatR;

namespace InsuranceApp.Application.Features.Clients.Querries.GetClients;
public class GetClientsHandler(IClientRepository clientRepository,IPolicyRepository policyRepository ,IMapper mapper)
    : IRequestHandler<GetClientsQuery, IReadOnlyList<ClientDto>>
{
    public async Task<IReadOnlyList<ClientDto>> Handle(GetClientsQuery request, CancellationToken ct)
    {
        var clients = await clientRepository.GetAllAsync(ct);

        // fetch counts in one go
        var policyCounts = await policyRepository.GetPolicyCountsByClientAsync(ct);

        var dtos = clients.Select(c =>
        {
            var policiesCount = policyCounts.TryGetValue(c.Id, out var count) ? count : 0;

            return new ClientDto(
                c.Id,
                c.Type,
                c.FirstName,
                c.LastName,
                c.CompanyName,
                c.Email,
                c.PhoneMobile,
                c.City,
                policiesCount
            );
        }).ToList();

        return dtos;
    }
}
