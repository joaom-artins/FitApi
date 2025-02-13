using Fit.Application.DTOs.Requests.UserRefreshToken;
using Fit.Application.DTOs.Requests.Login;
using Fit.Application.DTOs.Responses.Login;

namespace Fit.Application.Interfaces.IServices.Login;

public interface ILoginService
{
    Task<LoginResponse> Login(LoginRequest request);
    Task<LoginResponse> LoginWithRefreshTokenAsync(UserRefreshTokenRequest request);
}
