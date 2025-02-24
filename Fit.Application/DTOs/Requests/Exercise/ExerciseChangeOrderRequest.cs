namespace Fit.Application.DTOs.Requests.Exercise;

public class ExerciseChangeOrderRequest
{
    public IEnumerable<Guid> ExerciseId { get; set; } = [];
}
