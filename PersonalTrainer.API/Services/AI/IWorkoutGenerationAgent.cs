using PersonalTrainer.API.Models.DTO;

namespace PersonalTrainer.API.Services.AI;

public interface IWorkoutGenerationAgent
{
    Task<WorkoutPlanResponse> GenerateAsync(UserWorkoutProfileRequest request);
}
