using InsuranceApp.Domain.Entities;

namespace InsuranceApp.Application.Features.Clients.DTOs;

public record ClientDetailDto(
    Guid Id,
    ClientType Type,
    string? FirstName,
    string? LastName,
    string? CompanyName,
    string Email,
    string PhoneMobile,
    string? City,
    DateTime CreatedAtUtc,
    DateTime UpdatedAtUtc
);
