using Fit.Application.Interfaces.IRepositories;
using Fit.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Fit.Infrastructure.Persistence.Repositories;

public class GenericRepository<T>(AppDbContext context) : IGenericRepository<T> where T : class
{
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await context.Set<T>().AsNoTracking().SingleOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);
    }

    public async Task<T?> GetByGenericPropertyAsync<TProperty>(string propertyName, TProperty value)
    {
        var propertyInfo = typeof(T).GetProperty(propertyName);
        if (propertyInfo is null)
        {
            return default!;
        }

        var parameter = Expression.Parameter(typeof(T), "x");

        var propertyAccess = Expression.Property(parameter, propertyInfo);

        var constant = Expression.Constant(value, typeof(TProperty));
        var equality = Expression.Equal(propertyAccess, constant);

        var lambda = Expression.Lambda<Func<T, bool>>(equality, parameter);

        return await context.Set<T>().AsNoTracking().FirstOrDefaultAsync(lambda);
    }

    public async Task<T?> GetByTwoGenericPropertiesUsingOrAsync<TProperty1, TProperty2>(
    string propertyName1, TProperty1 value1,
    string propertyName2, TProperty2 value2)
    {
        var propertyInfo1 = typeof(T).GetProperty(propertyName1);
        var propertyInfo2 = typeof(T).GetProperty(propertyName2);

        if (propertyInfo1 is null || propertyInfo2 is null)
        {
            return default!;
        }

        var parameter = Expression.Parameter(typeof(T), "x");

        var propertyAccess1 = Expression.Property(parameter, propertyInfo1);
        var propertyAccess2 = Expression.Property(parameter, propertyInfo2);

        var constant1 = Expression.Constant(value1, typeof(TProperty1));
        var constant2 = Expression.Constant(value2, typeof(TProperty2));

        var equality1 = Expression.Equal(propertyAccess1, constant1);
        var equality2 = Expression.Equal(propertyAccess2, constant2);

        var orExpression = Expression.OrElse(equality1, equality2);

        var lambda = Expression.Lambda<Func<T, bool>>(orExpression, parameter);

        return await context.Set<T>().AsNoTracking().FirstOrDefaultAsync(lambda);
    }

    public async Task<T?> GetByTwoGenericPropertiesUsingAndAsync<TProperty1, TProperty2>(
    string propertyName1, TProperty1 value1,
    string propertyName2, TProperty2 value2)
    {
        var propertyInfo1 = typeof(T).GetProperty(propertyName1);
        var propertyInfo2 = typeof(T).GetProperty(propertyName2);

        if (propertyInfo1 is null || propertyInfo2 is null)
        {
            return default!;
        }

        var parameter = Expression.Parameter(typeof(T), "x");

        var propertyAccess1 = Expression.Property(parameter, propertyInfo1);
        var propertyAccess2 = Expression.Property(parameter, propertyInfo2);

        var constant1 = Expression.Constant(value1, typeof(TProperty1));
        var constant2 = Expression.Constant(value2, typeof(TProperty2));

        var equality1 = Expression.Equal(propertyAccess1, constant1);
        var equality2 = Expression.Equal(propertyAccess2, constant2);

        var orExpression = Expression.AndAlso(equality1, equality2);

        var lambda = Expression.Lambda<Func<T, bool>>(orExpression, parameter);

        return await context.Set<T>().AsNoTracking().FirstOrDefaultAsync(lambda);
    }

    public async Task AddAsync(T t)
    {
        await context.Set<T>().AddAsync(t);
    }

    public void Update(T t)
    {
        context.Set<T>().Update(t);
    }

    public void Remove(T t)
    {
        context.Set<T>().Remove(t);
    }
}


