using FluentValidation;

namespace InsuranceApp.Application.Features.Policies.Commands.CreatePolicy;


public class CreatePolicyValidator : AbstractValidator<CreatePolicyCommand>
{
    public CreatePolicyValidator()
    {
        RuleFor(x => x.PolicyNumber)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.ClientId).NotEmpty();

        RuleFor(x => x.PremiumAmount)
            .GreaterThan(0).WithMessage("Premium must be greater than 0");

        RuleFor(x => x.StartDate)
            .LessThan(x => x.EndDate)
            .WithMessage("Start date must be before end date");

        RuleFor(x => x.EndDate)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("End date must be in the future");
    }
}
