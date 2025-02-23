using Fit.Application.DTOs.Requests.Workout;
using FluentValidation;

namespace Fit.Application.Validators.Workout;

public class WorkoutUpdateRequestValidator : AbstractValidator<WorkoutUpdateRequest>
{
    public WorkoutUpdateRequestValidator()
    {
        RuleFor(x => x.DaysOfWeek)
                   .NotEmpty().WithMessage("Dia da semana é um campo obrigatório!")
                   .IsInEnum().WithMessage("Dia inválido!");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nome do treino é um campo obrigatório!")
            .MinimumLength(5).WithMessage("Nome deve ter pelo menos 5 caracteres!")
            .MaximumLength(60).WithMessage("Nome deve ter pelo menos 60 caracteres!");
    }
}
