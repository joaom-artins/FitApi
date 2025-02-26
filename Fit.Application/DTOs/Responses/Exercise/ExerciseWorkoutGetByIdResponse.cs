using Fit.Domain.Enums;

namespace Fit.Application.DTOs.Responses.Exercise;

public class ExerciseWorkoutGetByIdResponse
{
    public Guid Id { get; set; }
    public Guid WorkoutId { get; set; } = default!;
    public string Exercise { get; set; } = string.Empty;
    public int Series { get; set; }
    public RepsEnum Reps { get; set; }
    public byte Order { get; set; }
}
