namespace PersonalTrainer.API.Models.DTO;

public class ExerciseResponse
{
    public string Name                 { get; set; } = "";
    public string Instructions         { get; set; } = "";
    public int?   DurationSeconds      { get; set; }
    public int?   RestSeconds          { get; set; }
    public int?   Sets                 { get; set; }
    public int?   Reps                 { get; set; }
    public string MusclesTargeted      { get; set; } = "";
    public string Difficulty           { get; set; } = "";
    public string EncouragementMessage { get; set; } = "";
}
