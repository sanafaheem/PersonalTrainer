namespace PersonalTrainer.API.Models.DTO;

public class UserWorkoutProfileResponse
{
    public int Id { get; set; }
    public string? UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public int Age { get; set; }
    public string FitnessLevel { get; set; } = string.Empty;
    public string Goal { get; set; } = string.Empty;
    public string FocusArea { get; set; } = string.Empty;
    public int DurationMinutes { get; set; }
    public List<string> Equipment { get; set; } = [];
    public string? HealthLimitations { get; set; }
    public DateTime CreatedAt { get; set; }
}
