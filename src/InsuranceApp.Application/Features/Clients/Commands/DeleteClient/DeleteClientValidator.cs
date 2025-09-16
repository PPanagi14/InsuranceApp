using FluentValidation;

namespace InsuranceApp.Application.Features.Clients.Commands.DeleteClient;

public class DeleteClientValidator : AbstractValidator<DeleteClientCommand>
{
    public DeleteClientValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Client Id is required");
    }
}
