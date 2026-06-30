using Microsoft.AspNetCore.Mvc;
using PersonalTrainer.API.Models.DTO;
using PersonalTrainer.API.Services;

namespace PersonalTrainer.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserWorkoutProfileController(
    IUserWorkoutProfileService profileService,
    ICurrentUserService currentUser,
    IWorkoutAIService workoutAI,
    IWorkoutPlanService workoutPlanService,
    ILogger<UserWorkoutProfileController> logger) : ControllerBase
{
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetByUserId(string userId)
    {
        var profile = await profileService.GetByUserIdAsync(userId);
        return profile is null ? NotFound() : Ok(profile);
    }

    [HttpGet("my-plans")]
    public async Task<IActionResult> GetMyPlans()
    {
        if (!currentUser.IsLoggedIn)
            return Unauthorized();

        var profile = await profileService.GetByUserIdAsync(currentUser.UserId!);
        if (profile is null)
            return Ok(new List<object>());

        var plans = await workoutPlanService.GetAllByProfileIdAsync(profile.Id);
        return Ok(plans);
    }

    [HttpPost("generate")]
    public async Task<IActionResult> Generate([FromBody] UserWorkoutProfileRequest request)
    {
        logger.LogInformation(
            "Generate called — IsLoggedIn: {IsLoggedIn}, AuthHeader: {HasAuth}",
            currentUser.IsLoggedIn,
            Request.Headers.ContainsKey("Authorization"));

        int? profileId = null;

        if (currentUser.IsLoggedIn)
        {
            request.UserId = currentUser.UserId;
            var profile = await profileService.SaveAsync(request);
            profileId = profile.Id;

            var existing = await workoutPlanService.GetLatestByProfileIdAsync(profile.Id);
            if (existing is not null)
                return Ok(existing);
        }

        try
        {
            var plan = await workoutAI.GenerateWorkoutPlanAsync(request);

            if (profileId.HasValue)
                return Ok(await workoutPlanService.SaveAsync(plan, profileId.Value));

            return Ok(plan);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("rate limit"))
        {
            return StatusCode(429, new { error = ex.Message });
        }
    }
}
