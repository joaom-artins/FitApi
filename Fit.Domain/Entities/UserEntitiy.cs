using Fit.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Fit.Domain.Entities;

public class UserEntitiy : IdentityUser<Guid>
{
    public string Name { get; set; } = string.Empty;
    public UserTypeEnum Type { get; set; }
}
