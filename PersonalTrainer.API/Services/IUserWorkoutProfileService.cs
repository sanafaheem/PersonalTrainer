using PersonalTrainer.API.Models.DTO;

namespace PersonalTrainer.API.Services;

public interface IUserWorkoutProfileService
{
    Task<UserWorkoutProfileResponse?> GetByUserIdAsync(string userId);
    Task<UserWorkoutProfileResponse> SaveAsync(UserWorkoutProfileRequest request);
}
