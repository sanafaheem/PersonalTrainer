namespace PersonalTrainer.API.Enums;
using System.ComponentModel.DataAnnotations;

public enum FocusArea
{
        [Display(Name ="Full body work out")]
    FullBody,
    [Display(Name ="Upper body workout")]
    UpperBody,
    [Display(Name ="Lower body workout")]
    LowerBody,
    [Display(Name = "Core workout")]
    Core,
    [Display(Name = "Cardio workout")]
    Cardio
}
