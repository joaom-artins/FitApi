namespace Fit.Application.Interfaces.IRepositories;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task<T?> GetByGenericPropertyAsync<TProperty>(string propertyName, TProperty value);
    Task<T?> GetByTwoGenericPropertiesUsingOrAsync<TProperty1, TProperty2>(
    string propertyName1, TProperty1 value1,
    string propertyName2, TProperty2 value2);
    Task<T?> GetByTwoGenericPropertiesUsingAndAsync<TProperty1, TProperty2>(
    string propertyName1, TProperty1 value1,
    string propertyName2, TProperty2 value2);
    Task AddAsync(T t);
    void Update(T t);
    void Remove(T t);
}
