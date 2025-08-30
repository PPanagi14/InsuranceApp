using FluentValidation;

namespace InsuranceApp.Application.Features.Policies.Commands.UpdatePolicy;

public class UpdatePolicyCommandValidator : AbstractValidator<UpdatePolicyCommand>
{
    public UpdatePolicyCommandValidator()
    {
        RuleFor(x => x.ClientId).NotEmpty();
        RuleFor(x => x.Insurer).NotEmpty().MaximumLength(200);
        RuleFor(x => x.PolicyNumber).NotEmpty().MaximumLength(100);
        RuleFor(x => x.StartDate).LessThan(x => x.EndDate)
            .WithMessage("Start date must be before end date");
        RuleFor(x => x.PremiumAmount).GreaterThan(0);
        RuleFor(x => x.Currency).NotEmpty().Length(3);
    }
}
