using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PersonalTrainer.API.Models;
using PersonalTrainer.API.Services;

namespace PersonalTrainer.Tests.Services;

/// <summary>
/// Pure unit tests — no database, no substitutes.
/// TokenService only reads IConfiguration and does cryptographic logic.
/// </summary>
public class TokenServiceTests
{
    private readonly TokenService  _service;
    private readonly IConfiguration _config;

    public TokenServiceTests()
    {
        // Use a real ConfigurationBuilder with in-memory values.
        // Much cleaner than mocking IConfiguration key-by-key.
        _config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["Jwt:Key"]               = "test-signing-key-minimum-32-characters-long",
                ["Jwt:Issuer"]            = "TestIssuer",
                ["Jwt:Audience"]          = "TestAudience",
                ["Jwt:DurationInMinutes"] = "15"
            })
            .Build();

        _service = new TokenService(_config);
    }

    // ── GenerateAccessToken ───────────────────────────────────────────────────

    [Fact]
    public void GenerateAccessToken_ReturnsNonEmptyString()
    {
        var user = BuildUser();

        var token = _service.GenerateAccessToken(user);

        Assert.False(string.IsNullOrWhiteSpace(token));
    }

    [Fact]
    public void GenerateAccessToken_ContainsUserIdClaim()
    {
        var user = BuildUser("user-42");

        var claims = ParseClaims(_service.GenerateAccessToken(user));

        Assert.Contains(claims, c => c.Type == JwtRegisteredClaimNames.Sub && c.Value == "user-42");
    }

    [Fact]
    public void GenerateAccessToken_ContainsEmailClaim()
    {
        var user = BuildUser(email: "john@example.com");

        var claims = ParseClaims(_service.GenerateAccessToken(user));

        Assert.Contains(claims, c => c.Type == JwtRegisteredClaimNames.Email && c.Value == "john@example.com");
    }

    [Fact]
    public void GenerateAccessToken_IsSignedWithConfiguredKey()
    {
        var user  = BuildUser();
        var token = _service.GenerateAccessToken(user);
        var key   = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

        var validationParams = new TokenValidationParameters
        {
            ValidateIssuer           = true,
            ValidateAudience         = true,
            ValidateLifetime         = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer              = _config["Jwt:Issuer"],
            ValidAudience            = _config["Jwt:Audience"],
            IssuerSigningKey         = key
        };

        // Will throw if the token is invalid — that's the assertion
        new JwtSecurityTokenHandler().ValidateToken(token, validationParams, out _);
    }

    // ── GenerateRefreshToken ──────────────────────────────────────────────────

    [Fact]
    public void GenerateRefreshToken_ReturnsNonEmptyToken()
    {
        var token = _service.GenerateRefreshToken();

        Assert.False(string.IsNullOrWhiteSpace(token.Token));
    }

    [Fact]
    public void GenerateRefreshToken_ExpiresInSevenDays()
    {
        var token = _service.GenerateRefreshToken();

        Assert.True(token.Expires > DateTime.UtcNow.AddDays(6));
        Assert.True(token.Expires < DateTime.UtcNow.AddDays(8));
    }

    [Fact]
    public void GenerateRefreshToken_IsActiveOnCreation()
    {
        var token = _service.GenerateRefreshToken();

        Assert.True(token.IsActive);
        Assert.False(token.IsExpired);
        Assert.False(token.IsRevoked);
    }

    // ── Helpers ───────────────────────────────────────────────────────────────

    private static AppUser BuildUser(string id = "user-1", string email = "test@example.com") => new()
    {
        Id       = id,
        Email    = email,
        UserName = email
    };

    private static IEnumerable<Claim> ParseClaims(string token) =>
        new JwtSecurityTokenHandler().ReadJwtToken(token).Claims;
}
