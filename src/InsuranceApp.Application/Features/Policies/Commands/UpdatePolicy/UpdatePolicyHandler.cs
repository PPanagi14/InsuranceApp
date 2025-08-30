using AutoMapper;
using InsuranceApp.Application.Common.Interfaces;
using InsuranceApp.Application.Features.Policies.DTOs;
using MediatR;

namespace InsuranceApp.Application.Features.Policies.Commands.UpdatePolicy;

public class UpdatePolicyHandler(IPolicyRepository repo, IUnitOfWork uow, IMapper mapper)
    : IRequestHandler<UpdatePolicyCommand, PolicyDetailDto>
{
    public async Task<PolicyDetailDto> Handle(UpdatePolicyCommand request, CancellationToken ct)
    {
        var policy = await repo.GetByIdAsync(request.Id, ct);
        if (policy is null)
            throw new KeyNotFoundException($"Policy with ID {request.Id} not found");

        policy.ClientId = request.ClientId;
        policy.Insurer = request.Insurer;
        policy.PolicyType = request.PolicyType;
        policy.PolicyNumber = request.PolicyNumber;
        policy.StartDate = request.StartDate;
        policy.EndDate = request.EndDate;
        policy.PremiumAmount = request.PremiumAmount;
        policy.Currency = request.Currency;
        policy.Status = request.Status;

        await repo.UpdateAsync(policy, ct);
        await uow.SaveChangesAsync(ct);

        return mapper.Map<PolicyDetailDto>(policy);
    }
}
