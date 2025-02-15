using Fit.Application.DTOs.Requests.User;
using Fit.Common;
using FluentValidation;

namespace Fit.Application.Validators.User;

public class UserCreateRequestValidator : AbstractValidator<UserCreateRequest>
{
    public UserCreateRequestValidator()
    {
        RuleFor(x => x.Email)
           .NotEmpty().WithMessage("Email é um campo obrigatório!")
           .EmailAddress().WithMessage("Email inválido!");

        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Nome de usuário é um campo obrigatório!")
            .MinimumLength(5).WithMessage("Nome de usuário deve ter pelo menos 5 caracteres!")
            .MaximumLength(50).WithMessage("Nome de usuário deve ter pelo menos 50 caracteres!");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nome é um campo obrigatório!")
            .MinimumLength(5).WithMessage("Nome deve ter pelo menos 5 caracteres!")
            .MaximumLength(50).WithMessage("Nome deve ter pelo menos 50 caracteres!");

        RuleFor(x => x.Type)
            .NotEmpty().WithMessage("Tipo de usuário é um campo obrigatório!")
            .IsInEnum().WithMessage("Tipo de usuário inválido!");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Senha é umcampo obrigatório!")
            .Matches(RegexStrings.Password).WithMessage("Senha deve ter pelo menos 1 caractere maiúsculo, 1 minúsculo e 1 especial");
    }
}
