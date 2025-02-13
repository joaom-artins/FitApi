using Fit.Application.DTOs.Requests.Login;
using FluentValidation;

namespace Fit.Application.Validators.Login;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
          .NotEmpty().WithMessage("Usuário é um campo obrigatório!");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Senha é um campo obrigatório!");
    }
}
