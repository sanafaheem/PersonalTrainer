using PersonalTrainer.API.Enums;

namespace PersonalTrainer.API.Models;

public class ExerciseEquipment
{
    public int Id { get; set; }

    public int ExerciseTemplateId { get; set; }

    public Equipment EquipmentType { get; set; }

    public ExerciseTemplate ExerciseTemplate { get; set; } = null!;
}