using Microsoft.AspNetCore.Identity;

namespace PersonalTrainer.API.Models;

public class AppUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; set; } = [];
}
