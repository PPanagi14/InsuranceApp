using AutoMapper;
using InsuranceApp.Application.Common.Interfaces;
using InsuranceApp.Application.Features.Clients.DTOs;
using InsuranceApp.Domain.Entities;
using MediatR;

namespace InsuranceApp.Application.Features.Clients.Querries.GetClientById;

public class GetClientByIdHandler(IClientRepository repo, IMapper mapper)
    : IRequestHandler<GetClientByIdQuery, ClientDetailDto?>
{
    public async Task<ClientDetailDto?> Handle(GetClientByIdQuery request, CancellationToken ct)
    {
        var client = await repo.GetByIdAsync(request.Id, ct);
        return client is null ? null : mapper.Map<ClientDetailDto>(client);
    }
}
