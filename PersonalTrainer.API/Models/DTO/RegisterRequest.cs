using System.ComponentModel.DataAnnotations;

namespace PersonalTrainer.API.Models.DTO;
public class RegisterRequest
{
    [Required]
    [MaxLength(60)]
    public string FirstName { get; set; } = string.Empty;
    [Required]
    [MaxLength(60)]
    public string LastName { get; set; } = string.Empty;
    [EmailAddress]
    [Required]
    public string Email { get; set; } = string.Empty;
    [Required]
    [MinLength(6)]
    public string Password { get; set; } = string.Empty;
}