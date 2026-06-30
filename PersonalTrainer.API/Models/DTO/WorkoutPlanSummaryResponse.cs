namespace PersonalTrainer.API.Models.DTO;

public class WorkoutPlanSummaryResponse
{
    public int      Id                { get; set; }
    public string   Title             { get; set; } = "";
    public string   MotivationalIntro { get; set; } = "";
    public int      ExerciseCount     { get; set; }
    public DateTime CreatedAt         { get; set; }
}
