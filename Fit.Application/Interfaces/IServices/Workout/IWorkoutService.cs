using Fit.Application.DTOs.Requests.Workout;
using Fit.Application.DTOs.Responses.Workout;

namespace Fit.Application.Interfaces.IServices.Workout;

public interface IWorkoutService
{
    Task<WorkoutGetByIdResponse> GetByIdAsync(Guid id);
    Task<bool> CreateAsync(WorkoutCreateRequest request);
    Task<bool> UpdateAsync(Guid id, WorkoutUpdateRequest request);
    Task<bool> DeleteAsync(Guid id);
}
