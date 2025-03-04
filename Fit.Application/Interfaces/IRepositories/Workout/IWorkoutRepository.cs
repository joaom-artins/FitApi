using Fit.Domain.Entities;

namespace Fit.Application.Interfaces.IRepositories.Workout;

public interface IWorkoutRepository : IGenericRepository<WorkoutModel>
{
    Task<IEnumerable<WorkoutModel>> FindByUserAsync(Guid userId);
    Task<WorkoutModel?> GetByIdAnCreatorAsync(Guid id, Guid userId);
    Task<WorkoutModel?> GetByIdAndUserWithExerciseAsync(Guid id, Guid userId);
}
