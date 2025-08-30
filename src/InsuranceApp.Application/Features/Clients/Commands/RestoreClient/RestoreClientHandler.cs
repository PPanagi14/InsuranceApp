using InsuranceApp.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApp.Application.Features.Clients.Commands.RestoreClient;

public class RestoreClientHandler(IClientRepository repo, IUnitOfWork uow)
    : IRequestHandler<RestoreClientCommand, bool>
{
    public async Task<bool> Handle(RestoreClientCommand request, CancellationToken ct)
    {
        // Get client including deleted (IgnoreQueryFilters)
        var client = await repo.GetByIdAsync(request.Id, ct);

        if (client is null || client.DeletedAtUtc == null)
            return false;

        await repo.RestoreAsync(client, ct);
        await uow.SaveChangesAsync(ct);

        return true;
    }
}
