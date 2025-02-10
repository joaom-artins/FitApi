using Fit.Domain.Enums;

namespace Fit.Application.DTOs.Requests.User;

public class UserCreateRequest
{
    public string Name { get; set; } = string.Empty;
    public UserTypeEnum Type { get; set; }
}
