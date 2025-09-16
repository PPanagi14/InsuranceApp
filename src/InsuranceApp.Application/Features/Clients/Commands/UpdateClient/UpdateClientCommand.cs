using InsuranceApp.Application.Features.Clients.DTOs;
using InsuranceApp.Domain.Entities;
using MediatR;

namespace InsuranceApp.Application.Features.Clients.Commands.UpdateClient;

public record UpdateClientCommand(
    Guid Id,
    ClientType Type,
    string Email,
    string PhoneMobile,
    string? FirstName,
    string? LastName,
    string? CompanyName,
    string? VatNumber,
    string? Street,
    string? City,
    string? PostalCode,
    string? Country,
    DateTime? DateOfBirth,
    string? Notes
) : IRequest<ClientDetailDto>;
