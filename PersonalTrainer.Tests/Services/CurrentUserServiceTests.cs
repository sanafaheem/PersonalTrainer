using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using PersonalTrainer.API.Services;

namespace PersonalTrainer.Tests.Services;

/// <summary>
/// Pure unit tests — no database needed.
/// CurrentUserService only reads claims from IHttpContextAccessor.
/// </summary>
public class CurrentUserServiceTests
{
    // ── IsLoggedIn ────────────────────────────────────────────────────────────

    [Fact]
    public void IsLoggedIn_WhenUserIsAuthenticated_ReturnsTrue()
    {
        var service = BuildService(userId: "user-1");

        Assert.True(service.IsLoggedIn);
    }

    [Fact]
    public void IsLoggedIn_WhenHttpContextIsNull_ReturnsFalse()
    {
        var accessor = Substitute.For<IHttpContextAccessor>();
        accessor.HttpContext.Returns((HttpContext?)null);
        var service = new CurrentUserService(accessor);

        Assert.False(service.IsLoggedIn);
    }

    [Fact]
    public void IsLoggedIn_WhenIdentityIsNotAuthenticated_ReturnsFalse()
    {
        // An unauthenticated identity has no authenticationType
        var identity  = new ClaimsIdentity();
        var principal = new ClaimsPrincipal(identity);
        var context   = new DefaultHttpContext { User = principal };
        var accessor  = Substitute.For<IHttpContextAccessor>();
        accessor.HttpContext.Returns(context);
        var service = new CurrentUserService(accessor);

        Assert.False(service.IsLoggedIn);
    }

    // ── UserId ────────────────────────────────────────────────────────────────

    [Fact]
    public void UserId_WhenAuthenticated_ReturnsCorrectId()
    {
        var service = BuildService(userId: "user-42");

        Assert.Equal("user-42", service.UserId);
    }

    [Fact]
    public void UserId_WhenNotAuthenticated_ReturnsNull()
    {
        var accessor = Substitute.For<IHttpContextAccessor>();
        accessor.HttpContext.Returns((HttpContext?)null);
        var service = new CurrentUserService(accessor);

        Assert.Null(service.UserId);
    }

    // ── Helpers ───────────────────────────────────────────────────────────────

    private static CurrentUserService BuildService(string userId)
    {
        var claims    = new[] { new Claim(ClaimTypes.NameIdentifier, userId) };
        var identity  = new ClaimsIdentity(claims, authenticationType: "Test");
        var principal = new ClaimsPrincipal(identity);
        var context   = new DefaultHttpContext { User = principal };

        var accessor  = Substitute.For<IHttpContextAccessor>();
        accessor.HttpContext.Returns(context);

        return new CurrentUserService(accessor);
    }
}
