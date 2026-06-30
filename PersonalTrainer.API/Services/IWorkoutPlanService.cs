using PersonalTrainer.API.Models;
using PersonalTrainer.API.Models.DTO;

namespace PersonalTrainer.API.Services;

public interface IWorkoutPlanService
{
    Task<List<WorkoutPlanSummaryResponse>> GetAllByProfileIdAsync(int profileId);
    Task<WorkoutPlanResponse?> GetByIdAsync(int planId);
    Task<WorkoutPlanResponse?> GetLatestByProfileIdAsync(int profileId);
    Task<WorkoutPlanResponse> SaveAsync(WorkoutPlanResponse plan, int profileId);
}
