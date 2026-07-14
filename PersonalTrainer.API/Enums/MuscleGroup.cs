using System.ComponentModel.DataAnnotations;

namespace PersonalTrainer.API.Enums;

public enum MuscleGroup
{
    [Display(Name = "Chest")]
    Chest,

    [Display(Name = "Triceps")]
    Triceps,

    [Display(Name = "Shoulders")]
    Shoulders,

    [Display(Name = "Biceps")]
    Biceps,

    [Display(Name = "Back")]
    Back,

    [Display(Name = "Lats")]
    Lats,

    [Display(Name = "Core")]
    Core,

    [Display(Name = "Quads")]
    Quads,

    [Display(Name = "Hamstrings")]
    Hamstrings,

    [Display(Name = "Glutes")]
    Glutes,

    [Display(Name = "Calves")]
    Calves,

    [Display(Name = "Full Body")]
    FullBody
}
