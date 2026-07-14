using PersonalTrainer.API.Enums;

namespace PersonalTrainer.API.Mappers;

public static class FocusAreaMapper
{
    // Returns null for FullBody — means skip the muscle group filter entirely (all exercises qualify)
    public static IReadOnlyList<MuscleGroup>? ToMuscleGroups(FocusArea focusArea) => focusArea switch
    {
        FocusArea.UpperBody => [MuscleGroup.Chest, MuscleGroup.Triceps, MuscleGroup.Shoulders, MuscleGroup.Biceps, MuscleGroup.Back, MuscleGroup.Lats],
        FocusArea.LowerBody => [MuscleGroup.Quads, MuscleGroup.Hamstrings, MuscleGroup.Glutes, MuscleGroup.Calves],
        FocusArea.Core      => [MuscleGroup.Core, MuscleGroup.Obliques],
        FocusArea.Cardio    => [MuscleGroup.FullBody],
        FocusArea.FullBody  => null,
        _                   => null
    };
}
