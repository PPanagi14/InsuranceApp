using FluentValidation;
using InsuranceApp.Domain.Entities;

namespace InsuranceApp.Application.Features.Clients.Commands.UpdateClient;

public class UpdateClientCommandValidator : AbstractValidator<UpdateClientCommand>
{
    public UpdateClientCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().EmailAddress().MaximumLength(200);

        RuleFor(x => x.PhoneMobile)
            .NotEmpty().MaximumLength(30);

        RuleFor(x => x.FirstName)
            .NotEmpty().When(x => x.Type == ClientType.Person);

        RuleFor(x => x.LastName)
            .NotEmpty().When(x => x.Type == ClientType.Person);

        RuleFor(x => x.CompanyName)
            .NotEmpty().When(x => x.Type == ClientType.Company);
    }
}
