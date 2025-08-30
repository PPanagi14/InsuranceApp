using MediatR;

namespace InsuranceApp.Application.Features.Policies.Commands.RestorePolicy;

public record RestorePolicyCommand(Guid Id) : IRequest<bool>;
