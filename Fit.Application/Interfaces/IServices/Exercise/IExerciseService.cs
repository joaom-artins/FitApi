using Fit.Application.DTOs.Requests.Exercise;

namespace Fit.Application.Interfaces.IServices.Exercise;

public interface IExerciseService
{
    Task<bool> CreateAsync(Guid workoutId, ExerciseCreateRequest request);
    Task<bool> AddToWorkoutAsync(Guid workoutId, ExerciseCreateRequest request);
    Task<bool> RemoveToWorkoutAsync(Guid id, Guid workoutId);
}
