using System.Data;
using Fit.Application.DTOs.Requests.Workout;
using Fit.Application.Validators.Exercise;
using FluentValidation;

namespace Fit.Application.Validators.Workout;

public class WorkoutCreateRequestValidator : AbstractValidator<WorkoutCreateRequest>
{
    public WorkoutCreateRequestValidator()
    {
        RuleForEach(x => x.Exercises)
            .NotEmpty().WithMessage("Exercícios é um campo obrigatório!")
            .SetValidator(new ExerciseCreateRequestValidator());

        RuleFor(x => x.DaysOfWeek)
            .NotEmpty().WithMessage("Dia da semana é um campo obrigatório!")
            .IsInEnum().WithMessage("Dia inválido!");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nome do treino é um campo obrigatório!")
            .MinimumLength(5).WithMessage("Nome deve ter pelo menos 5 caracteres!")
            .MaximumLength(60).WithMessage("Nome deve ter pelo menos 60 caracteres!");

        RuleFor(x => x.StudentId)
             .Must((workout, forId) =>
                 workout.IsForYou || forId != Guid.Empty)
             .WithMessage("O campo aluno é obrigatório quando o treino não é para você!");
    }
}
