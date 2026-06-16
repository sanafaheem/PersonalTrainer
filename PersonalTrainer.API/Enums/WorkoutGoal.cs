namespace PersonalTrainer.API.Enums;
using System.ComponentModel.DataAnnotations;

public enum WorkoutGoal
{
    [Display(Name="Weight loss")]
    WeightLoss,
    [Display(Name ="Muscle Gain")]
    MuscleGain,
    [Display(Name ="Build Indurance")]
    Endurance,
    [Display(Name ="Improve Flexibility")]
    Flexibility
}
