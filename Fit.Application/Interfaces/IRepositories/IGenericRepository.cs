namespace Fit.Application.Interfaces.IRepositories;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task<T?> GetByIdAndUserAsync(Guid id, Guid userId);
    Task<T?> GetByGenericPropertyAsync<TProperty>(string propertyName, TProperty value);
    Task AddAsync(T t);
    void Update(T t);
    void Remove(T t);
}
