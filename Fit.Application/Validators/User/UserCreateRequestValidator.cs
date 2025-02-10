using Fit.Application.DTOs.Requests.User;
using FluentValidation;

namespace Fit.Application.Validators.User;

public class UserCreateRequestValidator : AbstractValidator<UserCreateRequest>
{
    public UserCreateRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nome é um campo obrigatório!")
            .MinimumLength(5).WithMessage("Nome deve ter pelo menos 5 caracteres!")
            .MaximumLength(50).WithMessage("Nome deve ter pelo menos 50 caracteres!");

        RuleFor(x => x.Type)
            .NotEmpty().WithMessage("Tipo de usuário é um campo obrigatório!")
            .IsInEnum().WithMessage("Tipo de usuário inválido!");
    }
}
