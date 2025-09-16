using InsuranceApp.Application.Common.Interfaces;
using InsuranceApp.Domain.Entities;
using MediatR;

namespace InsuranceApp.Application.Features.Clients.Commands.CreateClient;

public class CreateClientHandler(IClientRepository repo, IUnitOfWork uow)
    : IRequestHandler<CreateClientCommand, Guid>
{
    public async Task<Guid> Handle(CreateClientCommand request, CancellationToken ct)
    {
        // 🔹 Enforce uniqueness
        if (await repo.ExistsByEmailAsync(request.Email, ct))
            throw new InvalidOperationException($"Email {request.Email} is already in use");

        if (request.Type == ClientType.Company &&
            !string.IsNullOrWhiteSpace(request.VatNumber) &&
            await repo.ExistsByVatNumberAsync(request.VatNumber, ct))
        {
            throw new InvalidOperationException($"VAT number {request.VatNumber} is already registered");
        }

        // 🔹 Build entity
        var client = new Client
        {
            Type = request.Type,

            FirstName = request.Type == ClientType.Person ? request.FirstName : null,
            LastName = request.Type == ClientType.Person ? request.LastName : null,
            DateOfBirth = request.Type == ClientType.Person ? request.DateOfBirth : null,

            CompanyName = request.Type == ClientType.Company ? request.CompanyName : null,
            VatNumber = request.Type == ClientType.Company ? request.VatNumber : null,

            Email = request.Email,
            PhoneMobile = request.PhoneMobile,
            Street = request.Street,
            City = request.City,
            PostalCode = request.PostalCode,
            Country = request.Country,

            Notes = request.Notes
        };

        await repo.AddAsync(client, ct);
        await uow.SaveChangesAsync(ct);

        return client.Id;
    }
}
