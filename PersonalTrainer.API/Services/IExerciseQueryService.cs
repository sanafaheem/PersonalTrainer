using PersonalTrainer.API.Enums;
using PersonalTrainer.API.Models.DTO;

namespace PersonalTrainer.API.Services;

public interface IExerciseQueryService
{
    Task<List<ExerciseTemplateResult>> GetExercisesForProfileAsync(
        FocusArea focusArea,
        List<Equipment> availableEquipment);
}
