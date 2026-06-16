using System.ComponentModel.DataAnnotations;

namespace PersonalTrainer.API.Enums;

public enum FitnessLevel
{
    [Display(Name ="Beginer Level")]
    Beginner,

    [Display(Name ="Intermediate Level")]
    Intermediate,
    [Display(Name ="Advanced Level")]
    Advanced
}
