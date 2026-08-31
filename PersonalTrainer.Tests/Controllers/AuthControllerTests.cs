using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using PersonalTrainer.API.Controllers;
using PersonalTrainer.API.Models.DTO;
using PersonalTrainer.API.Services;

namespace PersonalTrainer.Tests.Controllers;

public class AuthControllerTests
{
    private readonly IAuthService   _authService = Substitute.For<IAuthService>();
    private readonly AuthController _controller;

    public AuthControllerTests()
    {
        _controller = new AuthController(_authService);
    }

    // ── Register ──────────────────────────────────────────────────────────────

    [Fact]
    public async Task Register_WithNewEmail_ReturnsOkWithAuthResponse()
    {
        var request  = new RegisterRequest { FirstName = "John", LastName = "Doe", Email = "john@example.com", Password = "Test@123" };
        var response = new AuthResponse { Email = request.Email, Token = "jwt-token" };
        _authService.RegisterAsync(request).Returns(response);

        var result = await _controller.Register(request);

        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.Same(response, ok.Value);
    }

    [Fact]
    public async Task Register_WithExistingEmail_ReturnsConflict()
    {
        var request = new RegisterRequest { FirstName = "John", LastName = "Doe", Email = "taken@example.com", Password = "Test@123" };
        _authService.RegisterAsync(request).Returns((AuthResponse?)null);

        var result = await _controller.Register(request);

        Assert.IsType<ConflictObjectResult>(result);
    }

    // ── Login ─────────────────────────────────────────────────────────────────

    [Fact]
    public async Task Login_WithValidCredentials_ReturnsOkWithToken()
    {
        var request  = new LoginRequest { Email = "john@example.com", Password = "Test@123" };
        var response = new AuthResponse { Email = request.Email, Token = "jwt-token", RefreshToken = "refresh" };
        _authService.LoginAsync(request).Returns(response);

        var result = await _controller.Login(request);

        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.Same(response, ok.Value);
    }

    [Fact]
    public async Task Login_WithInvalidCredentials_ReturnsUnauthorized()
    {
        var request = new LoginRequest { Email = "john@example.com", Password = "wrong" };
        _authService.LoginAsync(request).Returns((AuthResponse?)null);

        var result = await _controller.Login(request);

        Assert.IsType<UnauthorizedObjectResult>(result);
    }

    // ── RefreshToken ──────────────────────────────────────────────────────────

    [Fact]
    public async Task RefreshToken_WithValidToken_ReturnsOk()
    {
        var response = new AuthResponse { Token = "new-jwt", RefreshToken = "new-refresh" };
        _authService.RefreshAsync("valid-refresh").Returns(response);

        var result = await _controller.RefreshToken("valid-refresh");

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task RefreshToken_WithExpiredOrInvalidToken_ReturnsUnauthorized()
    {
        _authService.RefreshAsync(Arg.Any<string>()).Returns((AuthResponse?)null);

        var result = await _controller.RefreshToken("expired-token");

        Assert.IsType<UnauthorizedObjectResult>(result);
    }

    // ── RevokeToken ───────────────────────────────────────────────────────────

    [Fact]
    public async Task RevokeToken_WithValidToken_ReturnsNoContent()
    {
        _authService.RevokeAsync("valid-token").Returns(true);

        var result = await _controller.RevokeToken("valid-token");

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task RevokeToken_WithInvalidOrAlreadyRevokedToken_ReturnsBadRequest()
    {
        _authService.RevokeAsync(Arg.Any<string>()).Returns(false);

        var result = await _controller.RevokeToken("bad-token");

        Assert.IsType<BadRequestObjectResult>(result);
    }

    // ── GetAllUsers ───────────────────────────────────────────────────────────

    [Fact]
    public async Task GetAllUsers_ReturnsOkWithUserList()
    {
        var users = new List<object> { new { Id = "1", Email = "a@example.com" } };
        _authService.GetAllUsersAsync().Returns(users);

        var result = await _controller.GetAllUsers();

        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.IsAssignableFrom<IEnumerable<object>>(ok.Value);
    }

    // ── ResetPassword ─────────────────────────────────────────────────────────

    [Fact]
    public async Task ResetPassword_WithExistingUser_ReturnsNoContent()
    {
        var request = new ResetPasswordRequest { Email = "john@example.com", NewPassword = "New@123" };
        _authService.ResetPasswordAsync(request).Returns(true);

        var result = await _controller.ResetPassword(request);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task ResetPassword_WithNonExistentUser_ReturnsNotFound()
    {
        var request = new ResetPasswordRequest { Email = "ghost@example.com", NewPassword = "New@123" };
        _authService.ResetPasswordAsync(request).Returns(false);

        var result = await _controller.ResetPassword(request);

        Assert.IsType<NotFoundObjectResult>(result);
    }
}
