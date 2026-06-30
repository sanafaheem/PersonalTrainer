using PersonalTrainer.API.Models.DTO;

namespace PersonalTrainer.API.Services;

public interface IWorkoutAIService
{
    Task<WorkoutPlanResponse> GenerateWorkoutPlanAsync(UserWorkoutProfileRequest request);
}
