using InsuranceApp.Domain.Entities;

namespace InsuranceApp.Application.Features.Policies.DTOs;

public record PolicyDetailDto(
    Guid Id,
    Guid ClientId,
    string Insurer,
    PolicyType PolicyType,
    string PolicyNumber,
    DateTime StartDate,
    DateTime EndDate,
    decimal PremiumAmount,
    string Currency,
    PolicyStatus Status,
    DateTime CreatedAtUtc,
    DateTime UpdatedAtUtc
);
