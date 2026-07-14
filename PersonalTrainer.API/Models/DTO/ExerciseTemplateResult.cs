namespace PersonalTrainer.API.Models.DTO;

public class ExerciseTemplateResult
{
    public string Name { get; set; } = "";
    public string Instructions { get; set; } = "";
    public string Contraindications { get; set; } = "";
    public int MinDurationSeconds { get; set; }
    public int MaxDurationSeconds { get; set; }
    public int? MinReps { get; set; }
    public int? MaxReps { get; set; }
    public int? MinSets { get; set; }
    public int? MaxSets { get; set; }
    public List<string> MuscleGroups { get; set; } = [];
    public List<string> Equipment { get; set; } = [];
}
