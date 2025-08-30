using InsuranceApp.Application.Common.Interfaces;
using MediatR;

namespace InsuranceApp.Application.Features.Clients.Commands.DeleteClient;

public class DeleteClientHandler(IClientRepository repo, IUnitOfWork uow)
    : IRequestHandler<DeleteClientCommand, bool>
{
    public async Task<bool> Handle(DeleteClientCommand request, CancellationToken ct)
    {
        var client = await repo.GetByIdAsync(request.Id, ct);
        if (client is null) return false;

        await repo.DeleteAsync(client, ct);
        await uow.SaveChangesAsync(ct);

        return true;
    }
}
