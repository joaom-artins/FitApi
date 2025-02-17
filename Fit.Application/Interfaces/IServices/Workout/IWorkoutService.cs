using Fit.Application.DTOs.Requests.Workout;

namespace Fit.Application.Interfaces.IServices.Workout;

public interface IWorkoutService
{
    Task<bool> CreateAsync(WorkoutCreateRequest request);
}
