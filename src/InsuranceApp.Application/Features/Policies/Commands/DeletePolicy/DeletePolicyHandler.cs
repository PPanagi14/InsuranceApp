using InsuranceApp.Application.Common.Interfaces;
using MediatR;

namespace InsuranceApp.Application.Features.Policies.Commands.DeletePolicy;

public class DeletePolicyHandler(IPolicyRepository repo, IUnitOfWork uow)
    : IRequestHandler<DeletePolicyCommand, bool>
{
    public async Task<bool> Handle(DeletePolicyCommand request, CancellationToken ct)
    {
        var policy = await repo.GetByIdAsync(request.Id, ct);
        if (policy is null) return false;

        await repo.DeleteAsync(policy, ct);
        await uow.SaveChangesAsync(ct);

        return true;
    }
}
