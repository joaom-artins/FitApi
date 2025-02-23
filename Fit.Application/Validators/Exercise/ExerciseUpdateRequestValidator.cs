using Fit.Application.DTOs.Requests.Exercise;
using FluentValidation;

namespace Fit.Application.Validators.Exercise;

public class ExerciseUpdateRequestValidator : AbstractValidator<ExerciseUpdateRequest>
{
    public ExerciseUpdateRequestValidator()
    {
        RuleFor(x => x.Exercise)
           .NotEmpty().WithMessage("Nome do exercício é um campo obrigatório!")
           .MinimumLength(5).WithMessage("Nome do exercício deve ter pelo menos 5 carecteres!")
           .MaximumLength(40).WithMessage("Nome do exercício deve ter pelo menos 40 caracteres!");

        RuleFor(x => x.Reps)
            .NotEmpty().WithMessage("Número de repetições é um campo obrigatório!")
            .IsInEnum().WithMessage("Número de repetições inválido!");

        RuleFor(x => x.Series)
            .NotEmpty().WithMessage("Número de series é um campo obrigatório!");
    }
}
