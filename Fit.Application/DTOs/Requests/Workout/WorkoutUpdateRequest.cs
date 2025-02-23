using Fit.Domain.Enums;

namespace Fit.Application.DTOs.Requests.Workout;

public class WorkoutUpdateRequest
{
    public string Name { get; set; } = string.Empty;
    public DaysOfWeekEnum DaysOfWeek { get; set; }
}
