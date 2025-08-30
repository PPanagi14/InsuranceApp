using FluentValidation;
using InsuranceApp.Domain.Entities;

namespace InsuranceApp.Application.Features.Clients.Commands.CreateClient;

public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
{
    public CreateClientCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().When(x => x.Type == ClientType.Person)
            .MaximumLength(120);

        RuleFor(x => x.LastName)
            .NotEmpty().When(x => x.Type == ClientType.Person)
            .MaximumLength(120);

        RuleFor(x => x.CompanyName)
            .NotEmpty().When(x => x.Type == ClientType.Company)
            .MaximumLength(200);

        RuleFor(x => x.Email)
            .NotEmpty().EmailAddress().MaximumLength(200);

        RuleFor(x => x.Phone)
            .NotEmpty().MaximumLength(30);
    }
}
