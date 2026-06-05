using System.ComponentModel.DataAnnotations;

namespace PersonalTrainer.API.Models;

public class RefreshToken
{
    public int Id { get; set; }

    [Required]
    public string Token { get; set; } = string.Empty;
    public DateTime Expires { get; set; }
    public DateTime Created { get; set; }
    public DateTime? RevokedAt { get; set; }

    public string AppUserId { get; set; } = string.Empty;
    public AppUser AppUser { get; set; } = null!;

     public bool IsExpired => DateTime.UtcNow >= Expires;
    public bool IsRevoked => RevokedAt != null;
    public bool IsActive => !IsExpired && !IsRevoked;
}
