using Fit.Application.Interfaces;
using Fit.Application.Interfaces.IServices.Exercise;
using Fit.Application.Interfaces.IServices.Login;
using Fit.Application.Interfaces.IServices.Users;
using Fit.Application.Interfaces.IServices.Workout;
using Fit.Application.Notification;
using Fit.Application.Services.Exercise;
using Fit.Application.Services.Login;
using Fit.Application.Services.Users;
using Fit.Application.Services.Workout;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fit.Application.Utils;

public static partial class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<INotificationContext, NotificationContext>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<IWorkoutService, WorkoutService>();
        services.AddScoped<IExerciseService, ExerciseService>();

        return services;
    }
}
