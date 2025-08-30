using InsuranceApp.Application.Features.Clients.DTOs;
using InsuranceApp.Domain.Entities;
using MediatR;

namespace InsuranceApp.Application.Features.Clients.Querries.GetClients;

public record GetClientsQuery() : IRequest<IReadOnlyList<ClientDto>>;
