using InsuranceApp.Application.Features.Policies.DTOs;
using MediatR;

namespace InsuranceApp.Application.Features.Policies.Queries.GetPoliciesByClientId;

public record GetPoliciesByClientIdQuery(Guid ClientId) : IRequest<IReadOnlyList<PolicyDto>>;