using InsuranceApp.Domain.Entities;

namespace InsuranceApp.Application.Features.Policies.DTOs;

public record PolicyDto(
    Guid Id,
    string Insurer,
    PolicyType PolicyType,
    string PolicyNumber,
    DateTime StartDate,
    DateTime EndDate,
    decimal PremiumAmount,
    string Currency,
    PolicyStatus Status
);
