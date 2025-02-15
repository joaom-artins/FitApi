using Fit.Domain.Entities;

namespace Fit.Application.Interfaces.IRepositories.User;

public interface IUserRepository : IGenericRepository<UserModel>
{
    Task<IEnumerable<UserModel>> GetByEmailOrUserNameAsync(string email, string username);
    Task<IEnumerable<UserModel>> GetByEmailOrUserNameExcludingIdAsync(Guid id, string email, string username);
}
