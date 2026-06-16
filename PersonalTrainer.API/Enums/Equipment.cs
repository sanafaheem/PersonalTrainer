using System.ComponentModel.DataAnnotations;

namespace PersonalTrainer.API.Enums;

public enum Equipment
{
     [Display(Name = "Dumbbells")]
    Dumbbells,

    [Display(Name = "Resistance Band")]
    ResistanceBand,

    [Display(Name = "Bodyweight")]
    Bodyweight,

    [Display(Name = "Pull-up Bar")]
    PullUpBar,

    [Display(Name = "Kettlebell")]
    Kettlebell,

    [Display(Name = "Cardio Machine")]
    CardioMachine,

    [Display(Name = "Mat")]
    Mat
}
