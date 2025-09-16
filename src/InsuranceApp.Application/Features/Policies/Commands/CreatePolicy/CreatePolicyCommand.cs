using InsuranceApp.Application.Features.Policies.DTOs;
using InsuranceApp.Domain.Entities;
using MediatR;

namespace InsuranceApp.Application.Features.Policies.Commands.CreatePolicy;

public record CreatePolicyCommand(
    Guid ClientId,
    string Insurer,
    PolicyType PolicyType,
    string PolicyNumber,
    DateTime StartDate,
    DateTime EndDate,
    decimal PremiumAmount,
    string Currency,
    PolicyStatus Status,
    decimal? CoverageAmount,
    PaymentFrequency PaymentFrequency,
    string? PaymentMethod,
    decimal? BrokerCommission,
    DateTime? RenewalDate
) : IRequest<PolicyDetailDto>;
