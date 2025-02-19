using Fit.Application.DTOs.Requests.Exercise;
using Fit.Application.Interfaces.IRepositories;
using Fit.Application.Interfaces.IServices.Exercise;
using Fit.Application.UnitOfWork;
using Fit.Domain.Entities;

namespace Fit.Application.Services.Exercise;

public class ExerciseService(
    IUnitOfWork _unitOfWork,
    IGenericRepository<ExerciseModel> _exerciseRepository
) : IExerciseService
{
    public async Task<bool> CreateAsync(Guid workoutId, ExerciseCreateRequest request)
    {
        var exerciseModel = new ExerciseModel
        {
            Reps = request.Reps,
            Series = request.Series,
            WorkoutId = workoutId,
            Exercise = request.Exercise,
        };
        await _exerciseRepository.AddAsync(exerciseModel);
        await _unitOfWork.CommitAsync();

        return true;
    }
}
