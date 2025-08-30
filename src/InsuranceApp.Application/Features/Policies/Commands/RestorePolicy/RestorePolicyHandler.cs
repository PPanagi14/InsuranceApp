using InsuranceApp.Application.Common.Interfaces;
using MediatR;

namespace InsuranceApp.Application.Features.Policies.Commands.RestorePolicy;

public class RestorePolicyHandler(IPolicyRepository repo, IUnitOfWork uow)
    : IRequestHandler<RestorePolicyCommand, bool>
{
    public async Task<bool> Handle(RestorePolicyCommand request, CancellationToken ct)
    {
        var policy = await repo.GetByIdAsync(request.Id, ct);

        if (policy is null || policy.DeletedAtUtc == null)
            return false;

        await repo.RestoreAsync(policy, ct);
        await uow.SaveChangesAsync(ct);

        return true;
    }
}
