using FluentValidation;

namespace InsuranceApp.Application.Features.Policies.Commands.DeletePolicy;

public class DeletePolicyValidator : AbstractValidator<DeletePolicyCommand>
{
    public DeletePolicyValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Policy Id is required");
    }
}
