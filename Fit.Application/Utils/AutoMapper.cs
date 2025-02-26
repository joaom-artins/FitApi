using AutoMapper;
using Fit.Application.DTOs.Responses.Exercise;
using Fit.Application.DTOs.Responses.User;
using Fit.Application.DTOs.Responses.Workout;
using Fit.Domain.Entities;

namespace Fit.Application.Utils;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<UserModel, UserBaseResponse>();

        CreateMap<WorkoutModel, WorkoutGetByIdResponse>();

        CreateMap<ExerciseModel, ExerciseWorkoutGetByIdResponse>();
    }
}
