using System.Security.Claims;

namespace PersonalTrainer.API.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    private ClaimsPrincipal? User => httpContextAccessor.HttpContext?.User;

    public bool IsLoggedIn => User?.Identity?.IsAuthenticated ?? false;

    public string? UserId => User?.FindFirstValue(ClaimTypes.NameIdentifier);
}
