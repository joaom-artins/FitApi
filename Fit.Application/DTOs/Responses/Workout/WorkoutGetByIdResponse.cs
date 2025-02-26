using Fit.Application.DTOs.Responses.Exercise;
using Fit.Application.DTOs.Responses.User;
using Fit.Domain.Enums;

namespace Fit.Application.DTOs.Responses.Workout;

public class WorkoutGetByIdResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public UserBaseResponse CreatedBy { get; set; } = default!;
    public UserBaseResponse For { get; set; } = default!;
    public DaysOfWeekEnum DaysOfWeek { get; set; }
    public IEnumerable<ExerciseWorkoutGetByIdResponse> Exercises { get; set; } = [];
}
