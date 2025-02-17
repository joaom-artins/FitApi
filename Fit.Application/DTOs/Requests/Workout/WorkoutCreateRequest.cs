using Fit.Application.DTOs.Requests.Exercise;
using Fit.Domain.Enums;

namespace Fit.Application.DTOs.Requests.Workout;

public class WorkoutCreateRequest
{
    public string Name { get; set; } = string.Empty;
    public DaysOfWeekEnum DaysOfWeek { get; set; }
    public bool IsForYou {get; set; }
    public Guid? StudentId { get; set; }
    public IEnumerable<ExerciseCreateRequest> Exercises { get; set; } = default!;
}
