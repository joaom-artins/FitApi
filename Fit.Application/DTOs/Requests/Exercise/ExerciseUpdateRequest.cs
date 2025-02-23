using Fit.Domain.Enums;

namespace Fit.Application.DTOs.Requests.Exercise;

public class ExerciseUpdateRequest
{
    public RepsEnum Reps { get; set; }
    public string Exercise { get; set; } = string.Empty;
    public int Series { get; set; }
}
