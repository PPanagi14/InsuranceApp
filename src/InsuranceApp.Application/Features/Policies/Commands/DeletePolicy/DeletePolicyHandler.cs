using InsuranceApp.Application.Common.Interfaces;
using InsuranceApp.Domain.Entities;
using MediatR;

namespace InsuranceApp.Application.Features.Policies.Commands.DeletePolicy;

public class DeletePolicyHandler(IPolicyRepository repo, IUnitOfWork uow)
    : IRequestHandler<DeletePolicyCommand, bool>
{
    public async Task<bool> Handle(DeletePolicyCommand request, CancellationToken ct)
    {
        var policy = await repo.GetByIdAsync(request.Id, ct);
        if (policy is null)
            return false;

        // 🚨 Business rule: prevent deleting expired or cancelled policies
        if (policy.Status is PolicyStatus.Expired or PolicyStatus.Cancelled)
            throw new InvalidOperationException("Expired or cancelled policies cannot be deleted");

        await repo.DeleteAsync(policy, ct); // soft delete (sets DeletedAtUtc etc.)
        await uow.SaveChangesAsync(ct);

        return true;
    }
}
