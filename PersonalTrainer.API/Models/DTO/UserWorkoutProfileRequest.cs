using System.ComponentModel.DataAnnotations;

namespace PersonalTrainer.API.Models.DTO;

public class UserWorkoutProfileRequest
{
    public string? UserId { get; set; }

    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public int Age { get; set; }

    [Required]
    public string FitnessLevel { get; set; } = string.Empty;

    [Required]
    public string Goal { get; set; } = string.Empty;

    [Required]
    public string FocusArea { get; set; } = string.Empty;

    [Required]
    public int DurationMinutes { get; set; }

    [Required]
    public List<string> Equipment { get; set; } = [];

    public string? HealthLimitations { get; set; }
}
