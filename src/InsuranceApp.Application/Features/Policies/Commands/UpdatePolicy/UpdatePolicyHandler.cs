using AutoMapper;
using InsuranceApp.Application.Common.Interfaces;
using InsuranceApp.Application.Features.Policies.DTOs;
using InsuranceApp.Domain.Entities;
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

        // 🔹 Enforce uniqueness (exclude current policy)
        if (await repo.ExistsByPolicyNumberAsync(request.PolicyNumber, request.Insurer, ct) &&
            (policy.PolicyNumber != request.PolicyNumber || policy.Insurer != request.Insurer))
        {
            throw new InvalidOperationException(
                $"Policy number {request.PolicyNumber} already exists for insurer {request.Insurer}"
            );
        }

        // 🔹 Business rules
        if (request.EndDate <= request.StartDate)
            throw new InvalidOperationException("End date must be after start date");

        if (request.PremiumAmount <= 0)
            throw new InvalidOperationException("Premium amount must be greater than zero");

        if (request.BrokerCommission is < 0 or > 100)
            throw new InvalidOperationException("Broker commission must be between 0% and 100%");

        // 🚨 Prevent re-activating expired/cancelled policies
        if (policy.Status is PolicyStatus.Expired or PolicyStatus.Cancelled &&
            request.Status == PolicyStatus.Active)
        {
            throw new InvalidOperationException("Expired or cancelled policies cannot be re-activated");
        }

        // ✅ Update fields
        policy.ClientId = request.ClientId;
        policy.Insurer = request.Insurer;
        policy.PolicyType = request.PolicyType;
        policy.PolicyNumber = request.PolicyNumber;
        policy.StartDate = request.StartDate;
        policy.EndDate = request.EndDate;
        policy.PremiumAmount = request.PremiumAmount;
        policy.Currency = request.Currency;
        policy.Status = request.Status;

        // New fields
        policy.CoverageAmount = request.CoverageAmount;
        policy.PaymentFrequency = request.PaymentFrequency;
        policy.PaymentMethod = request.PaymentMethod;
        policy.BrokerCommission = request.BrokerCommission;
        policy.RenewalDate = request.RenewalDate;

        await repo.UpdateAsync(policy, ct);
        await uow.SaveChangesAsync(ct);

        return mapper.Map<PolicyDetailDto>(policy);
    }
}