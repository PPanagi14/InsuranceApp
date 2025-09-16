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
        // 🔹 Enforce uniqueness: policy number + insurer
        if (await repo.ExistsByPolicyNumberAsync(request.PolicyNumber, request.Insurer, ct))
            throw new InvalidOperationException(
                $"Policy number {request.PolicyNumber} already exists for insurer {request.Insurer}"
            );

        // 🔹 Business rules
        if (request.EndDate <= request.StartDate)
            throw new InvalidOperationException("End date must be after start date");

        if (request.PremiumAmount <= 0)
            throw new InvalidOperationException("Premium amount must be greater than zero");

        if (request.BrokerCommission is < 0 or > 100)
            throw new InvalidOperationException("Broker commission must be between 0% and 100%");

        // 🚨 Auto-expire if EndDate < today
        var status = request.EndDate < DateTime.UtcNow
            ? PolicyStatus.Expired
            : request.Status;

        // 🔹 Build entity
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
            Status = status,

            // new fields
            CoverageAmount = request.CoverageAmount,
            PaymentFrequency = request.PaymentFrequency,
            PaymentMethod = request.PaymentMethod,
            BrokerCommission = request.BrokerCommission,
            RenewalDate = request.RenewalDate
        };

        await repo.AddAsync(policy, ct);
        await uow.SaveChangesAsync(ct);

        return mapper.Map<PolicyDetailDto>(policy);
    }
}