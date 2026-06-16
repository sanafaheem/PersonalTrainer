namespace PersonalTrainer.API.Models;

public class WorkoutPlan
{
    public int Id { get; set; }
    public int UserWorkoutProfileId { get; set; }
    public UserWorkoutProfile? UserWorkoutProfile { get; set; }

    public string Title { get; set; } = "";
    public string MotivationalIntro { get; set; } = "";
    public string WarmupCue { get; set; } = "";
    public string CooldownCue { get; set; } = "";
    public string CompletionMessage { get; set; } = "";
    public List<Exercise> Exercises { get; set; } = new();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
