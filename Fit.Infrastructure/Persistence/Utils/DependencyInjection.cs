﻿using Fit.Application.Interfaces.IRepositories;
using Fit.Application.Interfaces.IRepositories.User;
using Fit.Application.UnitOfWork;
using Fit.Infrastructure.Persistence.Context;
using Fit.Infrastructure.Persistence.Repositories;
using Fit.Infrastructure.Persistence.Repositories.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fit.Infrastructure.Persistence.Utils;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
             options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")!)
         );
        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
