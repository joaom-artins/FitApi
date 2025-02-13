using Fit.Application.Interfaces;
using Fit.Application.Interfaces.IServices.Login;
using Fit.Application.Interfaces.IServices.Users;
using Fit.Application.Notification;
using Fit.Application.Services.Login;
using Fit.Application.Services.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fit.Application.Utils;

public static partial class DependencyInjection
{
    public static IServiceCollection AddAPplication(this IServiceCollection services)
    {
        services.AddScoped<INotificationContext, NotificationContext>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ILoginService, LoginService>();

        return services;
    }
}
