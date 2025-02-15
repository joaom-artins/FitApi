using Fit.Common.LoggedUser;
using Fit.Common.LoggedUser.interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Fit.Common.Utils;

public static class DependencyInjection
{
    public static IServiceCollection AddCommons(this IServiceCollection services)
    {
        services.AddScoped<IGetLoggedUser, GetLoggedUser>();

        return services;
    }
}
