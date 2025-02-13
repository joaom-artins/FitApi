using Fit.Application.DTOs.Requests.Login;
using Fit.Application.DTOs.Requests.UserRefreshToken;
using Fit.Application.DTOs.Responses.Login;
using Fit.Application.Interfaces;
using Fit.Application.Interfaces.IRepositories;
using Fit.Application.Interfaces.IServices.Login;
using Fit.Application.Notification;
using Fit.Application.UnitOfWork;
using Fit.Common;
using Fit.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Fit.Application.Services.Login;

public class LoginService(
    INotificationContext _notificationContext,
    IGenericRepository<UserModel> _userRepository,
    IUnitOfWork _unitOfWork,
    SignInManager<UserModel> _signInManager,
    UserManager<UserModel> _userManager,
    AppSettings _appSettings,
    IGenericRepository<UserRefreshTokenModel> _userRefreshTokenRepository
) : ILoginService
{
    public async Task<LoginResponse> Login(LoginRequest request)
    {
        var userRecord = await _userRepository.GetByGenericPropertyAsync("Email",request.Email);
        if (userRecord is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.User.NotFound
            );
            return default!;
        }

        var result = await _signInManager.CheckPasswordSignInAsync(userRecord, request.Password, false);
        if (!result.Succeeded)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.User.InvalidData
            );
            return default!;
        }

        var accessToken = await GenerateAccessTokenAsync(userRecord);
        if (_notificationContext.HasNotifications)
        {
            return default!;
        }

        var refreshToken = await GenerateRefreshTokenWithoutValidationAsync(userRecord.Id);

        return new LoginResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddSeconds(_appSettings.Jwt.Expiration),
        };
    }

    public async Task<string> GenerateAccessTokenAsync(UserModel user)
    {
        var role = await _userManager.GetRolesAsync(user);
        if (role is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status401Unauthorized,
                title: NotificationTitle.Unauthorized,
                detail: NotificationMessage.User.InvalidData
            );
            return default!;
        }

        var tokenHandle = new JwtSecurityTokenHandler();

        var claims = new List<Claim>
        {
            new("userId", user!.Id.ToString()),
            new(ClaimTypes.Role, role.First()),
        };

        var key = Encoding.ASCII.GetBytes(_appSettings.Jwt.SecretKey);

        var token = tokenHandle.CreateToken(new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddSeconds(_appSettings.Jwt.Expiration),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        });

        return tokenHandle.WriteToken(token);
    }

    public async Task<string> GenerateRefreshTokenWithoutValidationAsync(Guid userId)
    {
        byte[] randomNumber = new byte[64];
        var refreshToken = "";
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            refreshToken = Convert.ToBase64String(randomNumber);
        }

        var record = await _userRefreshTokenRepository.GetByGenericPropertyAsync("UserId",userId);
        if (record is not null)
        {
            _userRefreshTokenRepository.Remove(record);
        }

        var userRefreshTokenModel = new UserRefreshTokenModel
        {
            UserId = userId,
            Token = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddSeconds(_appSettings.Jwt.RefreshTokenExpiration)
        };

        await _userRefreshTokenRepository.AddAsync(userRefreshTokenModel);
        await _unitOfWork.CommitAsync();

        return refreshToken;
    }

    public async Task<LoginResponse> LoginWithRefreshTokenAsync(UserRefreshTokenRequest request)
    {
        var record = await _userRefreshTokenRepository.GetByGenericPropertyAsync("Token", request.Token);
        if (record is null || record.ExpiresAt < DateTime.UtcNow)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status401Unauthorized,
                title: NotificationTitle.Unauthorized,
                detail: NotificationMessage.User.InvalidToken
            );
            return default!;
        }

        var refreshToken = await GenerateRefreshTokenWithoutValidationAsync(record.UserId);

        var accessToken = await GenerateAccessTokenAsync(record.User);
        if (_notificationContext.HasNotifications)
        {
            return default!;
        }

        return new LoginResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddSeconds(_appSettings.Jwt.RefreshTokenExpiration),
        };
    }
}
