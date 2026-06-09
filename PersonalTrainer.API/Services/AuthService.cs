using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersonalTrainer.API.Data;
using PersonalTrainer.API.Models;
using PersonalTrainer.API.Models.DTO;

namespace PersonalTrainer.API.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly AppDbContext _context;

    public AuthService(UserManager<AppUser> userManager, ITokenService tokenService, AppDbContext context)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _context = context;
    }

    public async Task<AuthResponse?> RegisterAsync(RegisterRequest request)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
            return null;

        var user = new AppUser
        {
            UserName = request.Email,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
            throw new InvalidOperationException(
                string.Join("; ", result.Errors.Select(e => e.Description)));

        return await BuildAuthResponseAsync(user);
    }

    public async Task<AuthResponse?> LoginAsync(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            return null;

        return await BuildAuthResponseAsync(user);
    }

    public async Task<AuthResponse?> RefreshAsync(string refreshToken)
    {
        var token = await _context.RefreshTokens
            .Include(t => t.AppUser)
            .FirstOrDefaultAsync(t => t.Token == refreshToken);

        if (token == null || !token.IsActive)
            return null;

        token.RevokedAt = DateTime.UtcNow;

        var newRefreshToken = _tokenService.GenerateRefreshToken();
        newRefreshToken.AppUserId = token.AppUserId;

        _context.RefreshTokens.Add(newRefreshToken);
        await _context.SaveChangesAsync();

        return new AuthResponse
        {
            Token = _tokenService.GenerateAccessToken(token.AppUser),
            Email = token.AppUser.Email!,
            FirstName = token.AppUser.FirstName ?? string.Empty,
            LastName = token.AppUser.LastName ?? string.Empty,
            RefreshToken = newRefreshToken.Token,
            RefreshTokenExpiration = newRefreshToken.Expires,
        };
    }

    public async Task<bool> RevokeAsync(string refreshToken)
    {
        var token = await _context.RefreshTokens
            .FirstOrDefaultAsync(t => t.Token == refreshToken);

        if (token == null || !token.IsActive)
            return false;

        token.RevokedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }

    private async Task<AuthResponse> BuildAuthResponseAsync(AppUser user)
    {
        var refreshToken = _tokenService.GenerateRefreshToken();
        refreshToken.AppUserId = user.Id;

        _context.RefreshTokens.Add(refreshToken);
        await _context.SaveChangesAsync();

        return new AuthResponse
        {
            Token = _tokenService.GenerateAccessToken(user),
            Email = user.Email!,
            FirstName = user.FirstName ?? string.Empty,
            LastName = user.LastName ?? string.Empty,
            RefreshToken = refreshToken.Token,
            RefreshTokenExpiration = refreshToken.Expires,
        };
    }
}
