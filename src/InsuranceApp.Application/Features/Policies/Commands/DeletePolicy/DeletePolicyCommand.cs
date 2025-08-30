using MediatR;

namespace InsuranceApp.Application.Features.Policies.Commands.DeletePolicy;

public record DeletePolicyCommand(Guid Id) : IRequest<bool>;
