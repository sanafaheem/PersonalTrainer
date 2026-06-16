using System.ComponentModel.DataAnnotations;
using PersonalTrainer.API.Enums;

namespace PersonalTrainer.API.Models;

public class UserWorkoutProfile
{
    public int Id { get; set; }

    // Null if guest user
    public string? UserId { get; set; }
    public AppUser? User { get; set; }

    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public int Age { get; set; }

    [Required]
    public FitnessLevel FitnessLevel { get; set; }

    [Required]
    public WorkoutGoal Goal { get; set; }

    [Required]
    public FocusArea FocusArea { get; set; }

    [Required]
    public int DurationMinutes { get; set; }

    [Required]
    public List<Equipment> Equipment { get; set; } = [];

    public string? HealthLimitations { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
