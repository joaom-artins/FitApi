using AutoMapper;
using Fit.Application.DTOs.Requests.Workout;
using Fit.Application.DTOs.Responses.Workout;
using Fit.Application.Interfaces;
using Fit.Application.Interfaces.IRepositories;
using Fit.Application.Interfaces.IRepositories.User;
using Fit.Application.Interfaces.IRepositories.Workout;
using Fit.Application.Interfaces.IServices.Exercise;
using Fit.Application.Interfaces.IServices.Workout;
using Fit.Application.Notification;
using Fit.Application.UnitOfWork;
using Fit.Common.LoggedUser.interfaces;
using Fit.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Fit.Application.Services.Workout;

public class WorkoutService(
    IGetLoggedUser _getLoggedUser,
    IMapper _mapper,
    INotificationContext _notificationContext,
    IUnitOfWork _unitOfWork,
    IWorkoutRepository _workoutRepository,
    IExerciseService _exerciseService,
    IUserRepository _userRepository
) : IWorkoutService
{
    public async Task<WorkoutGetByIdResponse> GetByIdAsync(Guid id)
    {
        var workout = await _workoutRepository.GetByIdAndUserWithExerciseAsync(id, _getLoggedUser.GetId());
        if (workout is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.Workout.NotFound
            );
            return default!;
        }

        workout.Exercises = workout.Exercises.OrderBy(x => x.Order).ToList();

        return _mapper.Map<WorkoutGetByIdResponse>(workout);
    }

    public async Task<bool> CreateAsync(WorkoutCreateRequest request)
    {
        if (!request.IsForYou)
        {
            var loggedUser = await _userRepository.GetByIdAsync(_getLoggedUser.GetId());
            if (loggedUser!.Type != Domain.Enums.UserTypeEnum.PersonalTrainer)
            {
                _notificationContext.SetDetails(
                    statusCode: StatusCodes.Status400BadRequest,
                    title: NotificationTitle.BadRequest,
                    detail: NotificationMessage.Workout.OnlyPersonal
                );
                return false;
            }

            var student = await _userRepository.GetByIdAsync(request.StudentId!.Value);
            if (student is null)
            {
                _notificationContext.SetDetails(
                    statusCode: StatusCodes.Status404NotFound,
                    title: NotificationTitle.NotFound,
                    detail: NotificationMessage.User.NotFound
                );
                return false;
            }
        }

        _unitOfWork.BeginTransaction();

        var record = new WorkoutModel
        {
            CreatedById = _getLoggedUser.GetId(),
            Name = request.Name,
            DaysOfWeek = request.DaysOfWeek,
            ForId = request.IsForYou ? _getLoggedUser.GetId() : request.StudentId!.Value
        };
        await _workoutRepository.AddAsync(record);
        await _unitOfWork.CommitAsync();

        byte order = 1;
        foreach (var exercise in request.Exercises)
        {
            await _exerciseService.CreateAsync(record.Id, exercise, order);
            order++;
        }

        await _unitOfWork.CommitAsync(true);

        return true;
    }

    public async Task<bool> UpdateAsync(Guid id, WorkoutUpdateRequest request)
    {
        var record = await _workoutRepository.GetByIdAnCreatorAsync(id, _getLoggedUser.GetId());
        if (record is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.Workout.NotFound
            );
            return false;
        }

        record.Name = request.Name;
        record.DaysOfWeek = request.DaysOfWeek;
        _workoutRepository.Update(record);
        await _unitOfWork.CommitAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var record = await _workoutRepository.GetByIdAnCreatorAsync(id, _getLoggedUser.GetId());
        if (record is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.Workout.NotFound
            );
            return false;
        }

        _workoutRepository.Remove(record);
        await _unitOfWork.CommitAsync();

        return true;
    }
}
