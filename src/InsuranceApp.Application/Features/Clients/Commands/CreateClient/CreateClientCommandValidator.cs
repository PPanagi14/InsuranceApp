using FluentValidation;
using InsuranceApp.Domain.Entities;

namespace InsuranceApp.Application.Features.Clients.Commands.CreateClient;

public class CreateClientValidator : AbstractValidator<CreateClientCommand>
{
    public CreateClientValidator()
    {
        RuleFor(x => x.Type).IsInEnum();

        When(x => x.Type == ClientType.Person, () =>
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required");
        });

        When(x => x.Type == ClientType.Company, () =>
        {
            RuleFor(x => x.CompanyName).NotEmpty().WithMessage("Company name is required");
        });

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress().WithMessage("Invalid email address");

        RuleFor(x => x.Phone)
            .NotEmpty()
            .Matches(@"^\+?\d{7,15}$")
            .WithMessage("Invalid phone number format");
    }
}
