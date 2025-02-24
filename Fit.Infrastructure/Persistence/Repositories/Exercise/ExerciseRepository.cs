using Fit.Application.Interfaces.IRepositories.Exercise;
using Fit.Domain.Entities;
using Fit.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Fit.Infrastructure.Persistence.Repositories.Exercise;

public class ExerciseRepository(
    AppDbContext context
) : GenericRepository<ExerciseModel>(context),
    IExerciseRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<ExerciseModel>> FindByIdAndWorkoutAndCreatorAsync(Guid workoutId, Guid userId)
    {
        return await _context.Excersises.AsNoTracking().Where(x => x.WorkoutId == workoutId && x.Workout.CreatedById == userId).ToListAsync();
    }

    public async Task<ExerciseModel?> GetByIdAndWorkoutAndCreatorAsync(Guid id, Guid workoutId, Guid userId)
    {
        return await _context.Excersises.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id && x.WorkoutId == workoutId && x.Workout.CreatedById == userId);
    }
}
