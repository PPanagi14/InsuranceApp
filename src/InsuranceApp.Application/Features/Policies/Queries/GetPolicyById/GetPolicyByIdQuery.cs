using InsuranceApp.Application.Features.Policies.DTOs;
using MediatR;

namespace InsuranceApp.Application.Features.Policies.Queries.GetPolicyById;

public record GetPolicyByIdQuery(Guid Id) : IRequest<PolicyDetailDto?>;
