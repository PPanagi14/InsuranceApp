using InsuranceApp.Application.Features.Policies.DTOs;
using InsuranceApp.Domain.Entities;
using MediatR;

namespace InsuranceApp.Application.Features.Policies.Commands.UpdatePolicy;

public record UpdatePolicyCommand(
    Guid Id,
    Guid ClientId,
    string Insurer,
    PolicyType PolicyType,
    string PolicyNumber,
    DateTime StartDate,
    DateTime EndDate,
    decimal PremiumAmount,
    string Currency,
    PolicyStatus Status
) : IRequest<PolicyDetailDto>;
