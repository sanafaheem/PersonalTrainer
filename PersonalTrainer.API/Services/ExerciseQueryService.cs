using Microsoft.EntityFrameworkCore;
using PersonalTrainer.API.Data;
using PersonalTrainer.API.Enums;
using PersonalTrainer.API.Mappers;
using PersonalTrainer.API.Models.DTO;

namespace PersonalTrainer.API.Services;

public class ExerciseQueryService(AppDbContext context) : IExerciseQueryService
{
    public async Task<List<ExerciseTemplateResult>> GetExercisesForProfileAsync(
        FocusArea focusArea,
        List<Equipment> availableEquipment)
    {
        var muscleGroupFilter = FocusAreaMapper.ToMuscleGroups(focusArea);

        var query = context.ExerciseTemplates
            .Include(e => e.MuscleGroups)
            .Include(e => e.Equipment)
            .Where(e => e.IsActive)
            .Where(e => e.Equipment.Any(eq => availableEquipment.Contains(eq.EquipmentType)));

        if (muscleGroupFilter != null)
            query = query.Where(e => e.MuscleGroups.Any(mg => muscleGroupFilter.Contains(mg.MuscleGroup)));

        var templates = await query.ToListAsync();

        return templates.Select(e => new ExerciseTemplateResult
        {
            Name                = e.Name,
            Instructions        = e.Instructions,
            Contraindications   = e.Contraindications,
            MinDurationSeconds  = e.MinDurationSeconds,
            MaxDurationSeconds  = e.MaxDurationSeconds,
            MinReps             = e.MinReps,
            MaxReps             = e.MaxReps,
            MinSets             = e.MinSets,
            MaxSets             = e.MaxSets,
            MuscleGroups        = e.MuscleGroups.Select(mg => mg.MuscleGroup.ToString()).ToList(),
            Equipment           = e.Equipment.Select(eq => eq.EquipmentType.ToString()).ToList()
        }).ToList();
    }
}
