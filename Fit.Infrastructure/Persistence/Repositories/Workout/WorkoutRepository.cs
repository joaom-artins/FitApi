using Fit.Application.Interfaces.IRepositories.Workout;
using Fit.Domain.Entities;
using Fit.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Fit.Infrastructure.Persistence.Repositories.Workout;

public class WorkoutRepository(
    AppDbContext context
) : GenericRepository<WorkoutModel>(context),
    IWorkoutRepository
{
    private readonly AppDbContext _context = context;

    public async Task<WorkoutModel?> GetByIdAnCreatorAsync(Guid id, Guid userId)
    {
        return await _context.Workouts.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id && x.CreatedById == userId);
    }

    public async Task<IEnumerable<WorkoutModel>> GetByIdAndUserWithExerciseAsync(Guid id, Guid userId)
    {
        return await _context.Workouts.AsNoTracking().Where(x => x.Id == id && (x.CreatedById == userId || x.ForId == userId)).Include(x => x.Exercises).ToListAsync();
    }
}
