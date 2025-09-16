using InsuranceApp.Application.Common.Interfaces;
using InsuranceApp.Domain.Entities;
using MediatR;

namespace InsuranceApp.Application.Features.Clients.Commands.DeleteClient;

public class DeleteClientHandler(IClientRepository repo, IUnitOfWork uow)
    : IRequestHandler<DeleteClientCommand, bool>
{
    public async Task<bool> Handle(DeleteClientCommand request, CancellationToken ct)
    {
        var client = await repo.GetByIdAsync(request.Id, ct);
        if (client is null)
            return false;

        // 🚨 Business rule: Cannot delete if client has active policies
        var hasActivePolicies = client.Policies.Any(p => p.Status == PolicyStatus.Active);
        if (hasActivePolicies)
            throw new InvalidOperationException("Client cannot be deleted while having active policies");

        await repo.DeleteAsync(client, ct);
        await uow.SaveChangesAsync(ct);

        return true;
    }
}
