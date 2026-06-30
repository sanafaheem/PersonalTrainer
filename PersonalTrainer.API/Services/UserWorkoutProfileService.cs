using Microsoft.EntityFrameworkCore;
using PersonalTrainer.API.Data;
using PersonalTrainer.API.Enums;
using PersonalTrainer.API.Models;
using PersonalTrainer.API.Models.DTO;

namespace PersonalTrainer.API.Services;

public class UserWorkoutProfileService(AppDbContext db) : IUserWorkoutProfileService
{
    public async Task<UserWorkoutProfileResponse?> GetByUserIdAsync(string userId)
    {
        var profile = await db.UserWorkoutProfiles
            .FirstOrDefaultAsync(p => p.UserId == userId);

        return profile is null ? null : ToResponse(profile);
    }

    public async Task<UserWorkoutProfileResponse> SaveAsync(UserWorkoutProfileRequest request)
    {
        var existing = await db.UserWorkoutProfiles
            .FirstOrDefaultAsync(p => p.UserId == request.UserId);

        if (existing is not null)
        {
            existing.FirstName        = request.FirstName;
            existing.Age              = request.Age;
            existing.FitnessLevel     = Enum.Parse<FitnessLevel>(request.FitnessLevel);
            existing.Goal             = Enum.Parse<WorkoutGoal>(request.Goal);
            existing.FocusArea        = Enum.Parse<FocusArea>(request.FocusArea);
            existing.DurationMinutes  = request.DurationMinutes;
            existing.Equipment        = [..request.Equipment.Select(Enum.Parse<Equipment>)];
            existing.HealthLimitations = request.HealthLimitations;

            await db.SaveChangesAsync();
            return ToResponse(existing);
        }

        var profile = new UserWorkoutProfile
        {
            UserId            = request.UserId,
            FirstName         = request.FirstName,
            Age               = request.Age,
            FitnessLevel      = Enum.Parse<FitnessLevel>(request.FitnessLevel),
            Goal              = Enum.Parse<WorkoutGoal>(request.Goal),
            FocusArea         = Enum.Parse<FocusArea>(request.FocusArea),
            DurationMinutes   = request.DurationMinutes,
            Equipment         = [..request.Equipment.Select(Enum.Parse<Equipment>)],
            HealthLimitations = request.HealthLimitations
        };

        db.UserWorkoutProfiles.Add(profile);
        await db.SaveChangesAsync();

        return ToResponse(profile);
    }

    private static UserWorkoutProfileResponse ToResponse(UserWorkoutProfile profile) => new()
    {
        Id               = profile.Id,
        UserId           = profile.UserId,
        FirstName        = profile.FirstName,
        Age              = profile.Age,
        FitnessLevel     = profile.FitnessLevel.ToString(),
        Goal             = profile.Goal.ToString(),
        FocusArea        = profile.FocusArea.ToString(),
        DurationMinutes  = profile.DurationMinutes,
        Equipment        = [..profile.Equipment.Select(e => e.ToString())],
        HealthLimitations = profile.HealthLimitations,
        CreatedAt        = profile.CreatedAt
    };
}
