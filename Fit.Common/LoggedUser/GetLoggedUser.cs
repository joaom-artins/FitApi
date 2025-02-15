using System.Security.Claims;
using Fit.Common.LoggedUser.interfaces;
using Microsoft.AspNetCore.Http;

namespace Fit.Common.LoggedUser;

public class GetLoggedUser(
    IHttpContextAccessor _httpContextAccessor
) : IGetLoggedUser
{
    public Guid GetId()
    {
        return Guid.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue("userId")!);
    }
}
