using Fit.Domain.Enums;

namespace Fit.Application.DTOs.Responses.User;

public class UserBaseResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public UserTypeEnum Type { get; set; }
}
