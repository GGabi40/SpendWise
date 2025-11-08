using System.Security.Claims;
using SpendWise.Core.Interfaces;

namespace SpendWise.Web.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int? UserId
    {
        get
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)
                ?? _httpContextAccessor.HttpContext?.User?.FindFirst("sub")
                ?? _httpContextAccessor.HttpContext?.User?.FindFirst("id")
                ?? _httpContextAccessor.HttpContext?.User?.FindFirst("userId");

            return int.TryParse(userIdClaim?.Value, out int userId) ? userId : null;
        }
    }
}
