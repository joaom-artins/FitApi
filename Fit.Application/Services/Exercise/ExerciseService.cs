using Fit.Application.DTOs.Requests.Exercise;
using Fit.Application.Interfaces;
using Fit.Application.Interfaces.IRepositories.Exercise;
using Fit.Application.Interfaces.IRepositories.Workout;
using Fit.Application.Interfaces.IServices.Exercise;
using Fit.Application.Notification;
using Fit.Application.UnitOfWork;
using Fit.Common.LoggedUser.interfaces;
using Fit.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Fit.Application.Services.Exercise;

public class ExerciseService(
    INotificationContext _notificationContext,
    IGetLoggedUser _getLoggedUser,
    IUnitOfWork _unitOfWork,
    IWorkoutRepository _workoutRepository,
    IExerciseRepository _exerciseRepository
) : IExerciseService
{
    public async Task<bool> CreateAsync(Guid workoutId, ExerciseCreateRequest request, byte order)
    {
        var exerciseModel = new ExerciseModel
        {
            Reps = request.Reps,
            Series = request.Series,
            WorkoutId = workoutId,
            Exercise = request.Exercise,
            Order = order,
        };
        await _exerciseRepository.AddAsync(exerciseModel);
        await _unitOfWork.CommitAsync();

        return true;
    }

    public async Task<bool> AddToWorkoutAsync(Guid workoutId, ExerciseCreateRequest request)
    {
        var workout = await _workoutRepository.GetByIdAnCreatorAsync(workoutId, _getLoggedUser.GetId());
        if (workout is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.Workout.NotFound
            );
            return false;
        }

        var exercises = await _exerciseRepository.FindByIdAndWorkoutAndCreatorAsync(workoutId, _getLoggedUser.GetId());
        int order = exercises.Count() + 1;

        await CreateAsync(workoutId, request, (byte)order);

        return true;
    }

    public async Task<bool> RemoveToWorkoutAsync(Guid id, Guid workoutId)
    {
        var exercises = await _exerciseRepository.FindByIdAndWorkoutAndCreatorAsync(workoutId, _getLoggedUser.GetId());
        if (!exercises.Any())
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.Exercise.NotFound
            );
            return false;
        }

        var exerciseToExclude = exercises.SingleOrDefault(x => x.Id == id);
        if (exerciseToExclude is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.Exercise.NotFound
            );
            return false;
        }
        _unitOfWork.BeginTransaction();

        if (exerciseToExclude.Order != exercises.Count())
        {
            foreach (var exercise in exercises.Where(x => x.Order > exerciseToExclude.Order))
            {
                exercise.Order--;
                _exerciseRepository.Update(exercise);
                await _unitOfWork.CommitAsync();

            }
        }

        _exerciseRepository.Remove(exerciseToExclude);
        await _unitOfWork.CommitAsync(true);

        return true;
    }

    public async Task<bool> UpdateAsync(Guid id, Guid workoutId, ExerciseUpdateRequest request)
    {
        var record = await _exerciseRepository.GetByIdAndWorkoutAndCreatorAsync(id, workoutId, _getLoggedUser.GetId());
        if (record is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.Exercise.NotFound
            );
            return false;
        }

        record.Reps = request.Reps;
        record.Exercise = request.Exercise;
        record.Series = request.Series;
        _exerciseRepository.Update(record);
        await _unitOfWork.CommitAsync();

        return true;
    }
}
