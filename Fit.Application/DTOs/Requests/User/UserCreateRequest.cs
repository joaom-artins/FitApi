using Fit.Domain.Enums;

namespace Fit.Application.DTOs.Requests.User;

public class UserCreateRequest
{
    public string Name { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public UserTypeEnum Type { get; set; }
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}
