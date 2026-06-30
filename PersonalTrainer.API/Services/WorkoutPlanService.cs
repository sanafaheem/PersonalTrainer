using Microsoft.EntityFrameworkCore;
using PersonalTrainer.API.Data;
using PersonalTrainer.API.Models;
using PersonalTrainer.API.Models.DTO;

namespace PersonalTrainer.API.Services;

public class WorkoutPlanService(AppDbContext db) : IWorkoutPlanService
{
    public async Task<List<WorkoutPlanSummaryResponse>> GetAllByProfileIdAsync(int profileId)
    {
        return await db.WorkoutPlans
            .Where(p => p.UserWorkoutProfileId == profileId)
            .OrderByDescending(p => p.Id)
            .Select(p => new WorkoutPlanSummaryResponse
            {
                Id                = p.Id,
                Title             = p.Title,
                MotivationalIntro = p.MotivationalIntro,
                ExerciseCount     = p.Exercises.Count,
                CreatedAt         = p.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<WorkoutPlanResponse?> GetLatestByProfileIdAsync(int profileId)
    {
        var plan = await db.WorkoutPlans
            .Include(p => p.Exercises)
            .Where(p => p.UserWorkoutProfileId == profileId)
            .OrderByDescending(p => p.Id)
            .FirstOrDefaultAsync();

        return plan is null ? null : ToResponse(plan);
    }

    public async Task<WorkoutPlanResponse> SaveAsync(WorkoutPlanResponse dto, int profileId)
    {
        var plan = new WorkoutPlan
        {
            UserWorkoutProfileId = profileId,
            Title                = dto.Title,
            MotivationalIntro    = dto.MotivationalIntro,
            WarmupCue            = dto.WarmupCue,
            CooldownCue          = dto.CooldownCue,
            CompletionMessage    = dto.CompletionMessage,
            Exercises            = [..dto.Exercises.Select(e => new Exercise
            {
                Name                 = e.Name,
                Instructions         = e.Instructions,
                DurationSeconds      = e.DurationSeconds ?? 30,
                RestSeconds          = e.RestSeconds     ?? 15,
                Sets                 = e.Sets,
                Reps                 = e.Reps,
                MusclesTargeted      = e.MusclesTargeted,
                Difficulty           = e.Difficulty,
                EncouragementMessage = e.EncouragementMessage
            })]
        };

        db.WorkoutPlans.Add(plan);
        await db.SaveChangesAsync();
        return ToResponse(plan);
    }

    private static WorkoutPlanResponse ToResponse(WorkoutPlan plan) => new()
    {
        Id                = plan.Id,
        Title             = plan.Title,
        CreatedAt         = plan.CreatedAt,
        MotivationalIntro = plan.MotivationalIntro,
        WarmupCue         = plan.WarmupCue,
        CooldownCue       = plan.CooldownCue,
        CompletionMessage = plan.CompletionMessage,
        Exercises         = [..plan.Exercises.Select(e => new ExerciseResponse
        {
            Name                 = e.Name,
            Instructions         = e.Instructions,
            DurationSeconds      = e.DurationSeconds,
            RestSeconds          = e.RestSeconds,
            Sets                 = e.Sets,
            Reps                 = e.Reps,
            MusclesTargeted      = e.MusclesTargeted,
            Difficulty           = e.Difficulty,
            EncouragementMessage = e.EncouragementMessage
        })]
    };
}
