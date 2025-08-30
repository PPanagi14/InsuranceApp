using AutoMapper;
using InsuranceApp.Application.Common.Interfaces;
using InsuranceApp.Application.Features.Clients.DTOs;
using InsuranceApp.Domain.Entities;
using MediatR;

namespace InsuranceApp.Application.Features.Clients.Querries.GetClients;

public class GetClientsHandler(IClientRepository repo, IMapper mapper)
    : IRequestHandler<GetClientsQuery, IReadOnlyList<ClientDto>>
{
    public async Task<IReadOnlyList<ClientDto>> Handle(GetClientsQuery request, CancellationToken ct)
    {
        var clients = await repo.GetAllAsync(ct);
        return mapper.Map<IReadOnlyList<ClientDto>>(clients);
    }
}
