using Fit.Application.DTOs.Requests.User;
using Fit.Application.Interfaces;
using Fit.Application.Interfaces.IRepositories;
using Fit.Application.Interfaces.IServices.Users;
using Fit.Application.Notification;
using Fit.Application.UnitOfWork;
using Fit.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Fit.Application.Services.Users;

public class UserService(
    INotificationContext _notificationContext,
    IUnitOfWork _unitOfWork,
    UserManager<UserModel> _userManager,
    IGenericRepository<UserModel> _userRepository
) : IUserService
{
    public async Task<bool> CreateAsync(UserCreateRequest request)
    {
        if (request.Password != request.ConfirmPassword)
        {
            _notificationContext.SetDetails(
               statusCode: StatusCodes.Status400BadRequest,
               title: NotificationTitle.BadRequest,
               detail: NotificationMessage.User.PasswordAreDifferent
            );
            return false;
        }

        var existsEmail = await _userRepository.GetByGenericPropertyAsync("Email", request.Email);
        if (existsEmail != null)
        {
            _notificationContext.SetDetails(
               statusCode: StatusCodes.Status409Conflict,
               title: NotificationTitle.Conflict,
               detail: NotificationMessage.User.EmailAlreadyExists
            );
            return false;
        }

        var existsUserName = await _userRepository.GetByGenericPropertyAsync("UserName", request.UserName);
        if (existsUserName != null)
        {
            _notificationContext.SetDetails(
               statusCode: StatusCodes.Status409Conflict,
               title: NotificationTitle.Conflict,
               detail: NotificationMessage.User.UserNameAlreadyExists
            );
            return false;
        }

        _unitOfWork.BeginTransaction();

        var result = await _userManager.CreateAsync(new UserModel
        {
            Email = request.Email,
            NormalizedUserName = request.UserName,
            Name = request.Name,
            UserName = request.UserName,
        }, request.Password);

        if (!result.Succeeded)
        {
            _notificationContext.SetDetails(
               statusCode: StatusCodes.Status400BadRequest,
               title: NotificationTitle.BadRequest,
               detail: NotificationMessage.Common.UnexpectedError
            );
            return false;
        }

        var user = await _userManager.FindByEmailAsync(request.Email);

        var role = await _userManager.AddToRoleAsync(user!, "USER");
        if (!role.Succeeded)
        {
            _notificationContext.SetDetails(
               statusCode: StatusCodes.Status400BadRequest,
               title: NotificationTitle.BadRequest,
               detail: NotificationMessage.Common.UnexpectedError
            );
            return false;
        }

        await _unitOfWork.CommitAsync(true);

        return true;
    }
}
