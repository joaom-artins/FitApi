using Fit.Application.DTOs.Requests.User;

namespace Fit.Application.Interfaces.IServices.Users;

public interface IUserService
{
    Task<bool> CreateAsync(UserCreateRequest request);
    Task<bool> UpdateAsync(UserUpdateRequest request);
}
