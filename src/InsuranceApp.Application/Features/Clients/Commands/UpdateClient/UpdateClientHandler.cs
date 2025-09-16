using AutoMapper;
using InsuranceApp.Application.Common.Interfaces;
using InsuranceApp.Application.Features.Clients.DTOs;
using InsuranceApp.Domain.Entities;
using MediatR;

namespace InsuranceApp.Application.Features.Clients.Commands.UpdateClient;

public class UpdateClientHandler(IClientRepository repo, IUnitOfWork uow, IMapper mapper)
    : IRequestHandler<UpdateClientCommand, ClientDetailDto>
{
    public async Task<ClientDetailDto> Handle(UpdateClientCommand request, CancellationToken ct)
    {
        var client = await repo.GetByIdAsync(request.Id, ct);
        if (client is null)
            throw new KeyNotFoundException($"Client with ID {request.Id} not found");

        client.Type = request.Type;

        // Person
        client.FirstName = request.Type == ClientType.Person ? request.FirstName : null;
        client.LastName = request.Type == ClientType.Person ? request.LastName : null;
        client.DateOfBirth = request.Type == ClientType.Person ? request.DateOfBirth : null;

        // Company
        client.CompanyName = request.Type == ClientType.Company ? request.CompanyName : null;
        client.VatNumber = request.Type == ClientType.Company ? request.VatNumber : null;

        // Contact
        client.Email = request.Email;
        client.PhoneMobile = request.PhoneMobile;
        client.Street = request.Street;
        client.City = request.City;
        client.PostalCode = request.PostalCode;
        client.Country = request.Country;

        // Notes
        client.Notes = request.Notes;

        await repo.UpdateAsync(client, ct);
        await uow.SaveChangesAsync(ct);

        return mapper.Map<ClientDetailDto>(client);
    }
}
