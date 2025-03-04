using Fit.Domain.Enums;

namespace Fit.Application.DTOs.Responses.Workout;

public class WorkoutFindResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DaysOfWeekEnum DaysOfWeek { get; set; }

}
