using InsuranceApp.Application.Features.Clients.DTOs;
using InsuranceApp.Domain.Entities;
using MediatR;

namespace InsuranceApp.Application.Features.Clients.Querries.GetClientById;

public record GetClientByIdQuery(Guid Id) : IRequest<ClientDetailDto?>;

