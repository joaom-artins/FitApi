using Fit.Application.DTOs.Requests.User;
using Fit.Common;
using FluentValidation;

namespace Fit.Application.Validators.User;

public class UserUpdatePasswordRequestValidator : AbstractValidator<UserUpdatePasswordRequest>
{
    public UserUpdatePasswordRequestValidator()
    {
        RuleFor(x => x.CurrentPassword)
            .NotEmpty().WithMessage("Senha atual é um campo obrigatório!");

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("Senha é um campo obrigatório!")
            .Matches(RegexStrings.Password).WithMessage("Senha deve ter pelo menos 1 caractere maiúsculo, 1 minúsculo e 1 especial");
    }
}
