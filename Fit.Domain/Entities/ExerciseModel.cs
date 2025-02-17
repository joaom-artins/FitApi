using Fit.Domain.Enums;

namespace Fit.Domain.Entities;

public class ExerciseModel
{
    public Guid Id { get; set; }
    public WorkoutModel Workout { get; set; } = default!;
    public Guid WorkoutId { get; set; } = default!;
    public string Exercise { get; set; } = string.Empty;
    public int Series { get; set; }
    public RepsEnum Reps { get; set; }
}
