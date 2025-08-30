using InsuranceApp.Application.Common.Interfaces;
using InsuranceApp.Domain.Entities;
using MediatR;

namespace InsuranceApp.Application.Features.Clients.Commands.CreateClient;

public class CreateClientHandler(IClientRepository repo, IUnitOfWork uow)
    : IRequestHandler<CreateClientCommand, Guid>
{
    public async Task<Guid> Handle(CreateClientCommand request, CancellationToken ct)
    {
        var client = new Client
        {
            Type = request.Type,
            FirstName = request.Type == ClientType.Person ? request.FirstName : null,
            LastName = request.Type == ClientType.Person ? request.LastName : null,
            CompanyName = request.Type == ClientType.Company ? request.CompanyName : null,
            Email = request.Email,
            PhoneMobile = request.Phone,
            City = request.City
        };

        await repo.AddAsync(client, ct);
        await uow.SaveChangesAsync(ct);

        return client.Id;
    }
}
