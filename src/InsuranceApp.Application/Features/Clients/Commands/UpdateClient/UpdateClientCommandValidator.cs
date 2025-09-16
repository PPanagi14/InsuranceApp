using FluentValidation;
using InsuranceApp.Domain.Entities;

namespace InsuranceApp.Application.Features.Clients.Commands.UpdateClient;

public class UpdateClientValidator : AbstractValidator<UpdateClientCommand>
{
    public UpdateClientValidator()
    {
        // Must specify which client to update
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Client Id is required");

        // Client type must be valid
        RuleFor(x => x.Type).IsInEnum();

        // Person-specific rules
        When(x => x.Type == ClientType.Person, () =>
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required");

            RuleFor(x => x.DateOfBirth)
                .NotNull().WithMessage("Date of birth is required")
                .LessThan(DateTime.UtcNow).WithMessage("Date of birth must be in the past")
                .GreaterThan(DateTime.UtcNow.AddYears(-120))
                .WithMessage("Date of birth is not realistic (too old)");
        });

        // Company-specific rules
        When(x => x.Type == ClientType.Company, () =>
        {
            RuleFor(x => x.CompanyName)
                .NotEmpty().WithMessage("Company name is required");

            RuleFor(x => x.VatNumber)
                .NotEmpty().WithMessage("VAT number is required");
        });

        // Contact info
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email address");

        RuleFor(x => x.PhoneMobile)
            .NotEmpty().WithMessage("Phone number is required")
            .Matches(@"^\+?\d{7,15}$")
            .WithMessage("Invalid phone number format");

        // Optional fields with limits
        RuleFor(x => x.Notes)
            .MaximumLength(1000).WithMessage("Notes cannot exceed 1000 characters");

        RuleFor(x => x.City)
            .MaximumLength(120).WithMessage("City name cannot exceed 120 characters");
    }
}
