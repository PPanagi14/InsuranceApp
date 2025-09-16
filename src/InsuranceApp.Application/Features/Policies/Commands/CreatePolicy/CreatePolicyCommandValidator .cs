using FluentValidation;
using InsuranceApp.Domain.Entities;

namespace InsuranceApp.Application.Features.Policies.Commands.CreatePolicy;

public class CreatePolicyValidator : AbstractValidator<CreatePolicyCommand>
{
    public CreatePolicyValidator()
    {
        RuleFor(x => x.ClientId)
            .NotEmpty().WithMessage("ClientId is required");

        RuleFor(x => x.Insurer)
            .NotEmpty().WithMessage("Insurer is required")
            .MaximumLength(120);

        RuleFor(x => x.PolicyNumber)
            .NotEmpty().WithMessage("Policy number is required")
            .MaximumLength(120);

        RuleFor(x => x.PolicyType).IsInEnum();
        RuleFor(x => x.Status).IsInEnum();

        RuleFor(x => x.StartDate)
            .LessThan(x => x.EndDate).WithMessage("Start date must be before end date");

        RuleFor(x => x.PremiumAmount)
            .GreaterThan(0).WithMessage("Premium must be greater than 0");

        RuleFor(x => x.Currency)
            .NotEmpty().Length(3).WithMessage("Currency must be a valid ISO code");

        RuleFor(x => x.CoverageAmount)
            .GreaterThanOrEqualTo(0).When(x => x.CoverageAmount.HasValue);

        RuleFor(x => x.BrokerCommission)
            .InclusiveBetween(0, 100).When(x => x.BrokerCommission.HasValue)
            .WithMessage("Broker commission must be between 0% and 100%");
    }
}
