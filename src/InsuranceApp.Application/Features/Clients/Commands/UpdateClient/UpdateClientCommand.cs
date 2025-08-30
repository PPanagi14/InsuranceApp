using InsuranceApp.Application.Features.Clients.DTOs;
using InsuranceApp.Domain.Entities;
using MediatR;

namespace InsuranceApp.Application.Features.Clients.Commands.UpdateClient;

public record UpdateClientCommand(
    Guid Id,
    ClientType Type,
    string? FirstName,
    string? LastName,
    string? CompanyName,
    string Email,
    string PhoneMobile,
    string? City
) : IRequest<ClientDetailDto>;
