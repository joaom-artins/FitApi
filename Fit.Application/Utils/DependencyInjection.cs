using Fit.Application.Interfaces;
using Fit.Application.Notification;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fit.Application.Utils;

public static partial class DependencyInjection
{
    public static IServiceCollection AddAPplication(this IServiceCollection services)
    {
        services.AddScoped<INotificationContext, NotificationContext>();

        return services;
    }
}
