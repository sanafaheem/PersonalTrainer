namespace PersonalTrainer.API.Models.DTO;

public class WorkoutPlanResponse
{
    public int                    Id                { get; set; }
    public string                 Title             { get; set; } = "";
    public string                 MotivationalIntro { get; set; } = "";
    public string                 WarmupCue         { get; set; } = "";
    public string                 CooldownCue       { get; set; } = "";
    public string                 CompletionMessage { get; set; } = "";
    public List<ExerciseResponse> Exercises         { get; set; } = [];
    public DateTime               CreatedAt         { get; set; }
}
