using Fit.Domain.Entities;

namespace Fit.Application.Interfaces.IRepositories.Exercise;

public interface IExerciseRepository : IGenericRepository<ExerciseModel>
{
    Task<ExerciseModel?> GetByIdAndWorkoutAndCreatorAsync(Guid id, Guid workoutId, Guid userId);
}
