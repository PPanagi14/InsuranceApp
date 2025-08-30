using AutoMapper;
using InsuranceApp.Application.Common.Interfaces;
using InsuranceApp.Application.Features.Policies.DTOs;
using InsuranceApp.Domain.Entities;
using MediatR;

namespace InsuranceApp.Application.Features.Policies.Commands.CreatePolicy;

public class CreatePolicyHandler(IPolicyRepository repo, IUnitOfWork uow, IMapper mapper)
    : IRequestHandler<CreatePolicyCommand, PolicyDetailDto>
{
    public async Task<PolicyDetailDto> Handle(CreatePolicyCommand request, CancellationToken ct)
    {
        var policy = new Policy
        {
            ClientId = request.ClientId,
            Insurer = request.Insurer,
            PolicyType = request.PolicyType,
            PolicyNumber = request.PolicyNumber,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            PremiumAmount = request.PremiumAmount,
            Currency = request.Currency,
            Status = request.Status
        };

        await repo.AddAsync(policy, ct);
        await uow.SaveChangesAsync(ct);

        return mapper.Map<PolicyDetailDto>(policy);
    }
}
