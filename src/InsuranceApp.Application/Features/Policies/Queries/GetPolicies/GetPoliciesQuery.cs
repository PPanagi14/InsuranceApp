using InsuranceApp.Application.Features.Policies.DTOs;
using MediatR;

namespace InsuranceApp.Application.Features.Policies.Queries.GetPolicies;

public record GetPoliciesQuery() : IRequest<IReadOnlyList<PolicyDto>>;
