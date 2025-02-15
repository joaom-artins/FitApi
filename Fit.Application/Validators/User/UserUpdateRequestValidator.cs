using Fit.Application.DTOs.Requests.User;
using FluentValidation;

namespace Fit.Application.Validators.User;

public class UserUpdateRequestValidator : AbstractValidator<UserUpdateRequest>
{
  public UserUpdateRequestValidator()
  {
    RuleFor(x => x.Email)
      .NotEmpty().WithMessage("Email é um campo obrigatório!")
      .EmailAddress().WithMessage("Email inválido!");

    RuleFor(x => x.Name)
        .NotEmpty().WithMessage("Nome é um campo obrigatório!")
        .MinimumLength(5).WithMessage("Nome deve ter pelo menos 5 caracteres!")
        .MaximumLength(50).WithMessage("Nome deve ter pelo menos 50 caracteres!");

    RuleFor(x => x.UserName)
      .NotEmpty().WithMessage("Nome de usuário é um campo obrigatório!")
      .MinimumLength(5).WithMessage("Nome de usuário deve ter pelo menos 5 caracteres!")
      .MaximumLength(50).WithMessage("Nome de usuário deve ter pelo menos 50 caracteres!");
  }
}
