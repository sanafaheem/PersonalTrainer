using System.ComponentModel.DataAnnotations;

namespace PersonalTrainer.API.Models
{
    public class ExerciseTemplate
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        // Duration range
        public int MinDurationSeconds { get; set; }
        public int MaxDurationSeconds { get; set; }

        // Reps range (nullable — not all exercises have reps)
        public int? MinReps { get; set; }
        public int? MaxReps { get; set; }

        // Sets range (nullable — not all exercises have sets)
        public int? MinSets { get; set; }
        public int? MaxSets { get; set; }

        [Required]
        [MaxLength(500)]
        public string Instructions { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Contraindications { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        // Navigation property
        public ICollection<ExerciseEquipment> Equipment { get; set; } = new List<ExerciseEquipment>();
        public ICollection<ExerciseMuscleGroup> MuscleGroups { get; set; } = new List<ExerciseMuscleGroup>();

    }
}
