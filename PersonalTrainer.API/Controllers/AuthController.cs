using Microsoft.AspNetCore.Mvc;
using PersonalTrainer.API.Services;
using PersonalTrainer.API.Models.DTO;

namespace PersonalTrainer.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService auth)
    {
        _authService = auth;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest register)
    {
        var res = await _authService.RegisterAsync(register);
        if (res == null)
            return Conflict("Email is already registered.");

        return Ok(res);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var res = await _authService.LoginAsync(request);
        if (res == null)
            return Unauthorized("Invalid email or password.");

        return Ok(res);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
    {
        var res = await _authService.RefreshAsync(refreshToken);
        if (res == null)
            return Unauthorized("Invalid or expired refresh token.");

        return Ok(res);
    }

    [HttpPost("revoke-token")]
    public async Task<IActionResult> RevokeToken([FromBody] string refreshToken)
    {
        var success = await _authService.RevokeAsync(refreshToken);
        if (!success)
            return BadRequest("Invalid or already revoked token.");

        return NoContent();
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _authService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpPatch("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
    {
        var success = await _authService.ResetPasswordAsync(request);
        if (!success)
            return NotFound("User with this email does not exist.");

        return NoContent();
    }
}
