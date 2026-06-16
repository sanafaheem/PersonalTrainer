using Microsoft.AspNetCore.Mvc;
using PersonalTrainer.API.Models;
using PersonalTrainer.API.Models.DTO;
using PersonalTrainer.API.Services;

namespace PersonalTrainer.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserWorkoutProfileController(
    IUserWorkoutProfileService profileService,
    ICurrentUserService currentUser) : ControllerBase
{
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetByUserId(string userId)
    {
        var profile = await profileService.GetByUserIdAsync(userId);
        return profile is null ? NotFound() : Ok(profile);
    }

    [HttpPost("generate")]
    public async Task<IActionResult> Generate([FromBody] UserWorkoutProfileRequest request)
    {
        if (currentUser.IsLoggedIn)
        {
            request.UserId = currentUser.UserId;
            await profileService.SaveAsync(request);
        }

        var mockPlan = new WorkoutPlan
        {
            Title = "Power & Endurance Blast",
            MotivationalIntro = $"Let's go {request.FirstName}! Today's session is built around your goals. Give it everything you've got!",
            WarmupCue = "Start with 5 minutes of light jogging or jumping jacks to get your blood flowing.",
            CooldownCue = "Finish with 5 minutes of gentle stretching, focusing on the muscles you worked today.",
            CompletionMessage = $"Amazing work {request.FirstName}! You crushed it today. Rest up and come back stronger!",
            Exercises =
            [
                new Exercise
                {
                    Name = "Push-Ups",
                    Instructions = "Start in a high plank position. Lower your chest to the floor, then push back up. Keep your core tight throughout.",
                    DurationSeconds = 0,
                    RestSeconds = 30,
                    Sets = 3,
                    Reps = 12,
                    MusclesTargeted = "Chest, Shoulders, Triceps",
                    Difficulty = "Beginner",
                    EncouragementMessage = "Great form! Keep that core engaged!"
                },
                new Exercise
                {
                    Name = "Bodyweight Squats",
                    Instructions = "Stand with feet shoulder-width apart. Lower your hips until thighs are parallel to the floor, then drive back up.",
                    DurationSeconds = 0,
                    RestSeconds = 30,
                    Sets = 3,
                    Reps = 15,
                    MusclesTargeted = "Quads, Glutes, Hamstrings",
                    Difficulty = "Beginner",
                    EncouragementMessage = "Feel the burn! You're building a strong foundation!"
                },
                new Exercise
                {
                    Name = "Plank Hold",
                    Instructions = "Hold a forearm plank with your body in a straight line from head to heels. Breathe steadily.",
                    DurationSeconds = 45,
                    RestSeconds = 20,
                    Sets = 3,
                    Reps = null,
                    MusclesTargeted = "Core, Shoulders, Glutes",
                    Difficulty = "Beginner",
                    EncouragementMessage = "Hold strong! Every second counts!"
                },
                new Exercise
                {
                    Name = "Jumping Jacks",
                    Instructions = "Jump your feet out while raising your arms overhead, then return to starting position. Keep a steady rhythm.",
                    DurationSeconds = 60,
                    RestSeconds = 15,
                    Sets = 3,
                    Reps = null,
                    MusclesTargeted = "Full Body, Cardiovascular",
                    Difficulty = "Beginner",
                    EncouragementMessage = "Keep the energy up! You're doing great!"
                }
            ]
        };

        return Ok(mockPlan);
    }
}
