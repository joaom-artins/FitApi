using Fit.Application.DTOs.Requests.User;
using Fit.Application.Interfaces;
using Fit.Application.Interfaces.IRepositories;
using Fit.Application.Interfaces.IServices.Users;
using Fit.Application.Notification;
using Fit.Application.UnitOfWork;
using Fit.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Fit.Common.LoggedUser.interfaces;
using Fit.Application.Interfaces.IRepositories.User;

namespace Fit.Application.Services.Users;

public class UserService(
    INotificationContext _notificationContext,
    IGetLoggedUser _getLoggedUser,
    IUnitOfWork _unitOfWork,
    UserManager<UserModel> _userManager,
    IUserRepository _userRepository
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

        await ExistsByEmailOrUserName(request.Email, request.UserName);
        if (_notificationContext.HasNotifications)
        {
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

    public async Task<bool> UpdateAsync(UserUpdateRequest request)
    {
        await ExistsByEmailOrUserNameExcludingId(_getLoggedUser.GetId(), request.Email, request.UserName);
        if (_notificationContext.HasNotifications)
        {
            return false;
        }


        var user = await _userRepository.GetByIdAsync(_getLoggedUser.GetId());
        if (user is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.User.NotFound
            );
            return false;
        }

        user.Name = request.Name;
        user.Email = request.Email;
        _userRepository.Update(user);
        await _unitOfWork.CommitAsync();

        return true;
    }

    private async Task<bool> ExistsByEmailOrUserNameExcludingId(Guid id, string email, string username)
    {
        var userExists = await _userRepository.GetByEmailOrUserNameExcludingIdAsync(id, email, username);
        if (userExists.Any())
        {
            if (userExists.Any(x => x.UserName == username))
            {
                _notificationContext.AddNotification("Username", NotificationMessage.User.UserNameAlreadyExists);
            }
            if (userExists.Any(x => x.Email == email))
            {
                _notificationContext.AddNotification("Email", NotificationMessage.User.EmailAlreadyExists);
            }

            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status409Conflict,
                title: NotificationTitle.Conflict,
                detail: NotificationMessage.Common.DataExists
            );
            return default!;
        }

        return true;
    }

    private async Task<bool> ExistsByEmailOrUserName(string email, string username)
    {
        var userExists = await _userRepository.GetByEmailOrUserNameAsync(email, username);
        if (userExists.Any())
        {
            if (userExists.Any(x => x.UserName == username))
            {
                _notificationContext.AddNotification("Username", NotificationMessage.User.UserNameAlreadyExists);
            }
            if (userExists.Any(x => x.Email == email))
            {
                _notificationContext.AddNotification("Email", NotificationMessage.User.EmailAlreadyExists);
            }

            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status409Conflict,
                title: NotificationTitle.Conflict,
                detail: NotificationMessage.Common.DataExists
            );
            return default!;
        }

        return true;
    }
}