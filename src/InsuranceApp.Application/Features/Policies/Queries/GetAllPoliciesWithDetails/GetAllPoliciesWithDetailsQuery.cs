using InsuranceApp.Application.Features.Policies.DTOs;
using MediatR;

namespace InsuranceApp.Application.Features.Policies.Queries.GetAllPoliciesWithDetails;

public record GetAllPoliciesWithDetailsQuery() : IRequest<IReadOnlyList<PolicyDetailDto>>;

