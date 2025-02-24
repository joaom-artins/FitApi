using Fit.Domain.Entities;

namespace Fit.Application.Interfaces.IRepositories.Workout;

public interface IWorkoutRepository : IGenericRepository<WorkoutModel>
{
    Task<WorkoutModel?> GetByIdAnCreatorAsync(Guid id, Guid userId);
}
