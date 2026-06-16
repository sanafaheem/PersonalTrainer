using Microsoft.AspNetCore.Mvc;
using PersonalTrainer.API.Enums;
using PersonalTrainer.API.Helpers;

namespace PersonalTrainer.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkoutOptionsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetOptions()
    {
        return Ok(new
        {
            fitnessLevels = Enum.GetValues<FitnessLevel>().Select(e => new { value = e.ToString(), displayName = e.GetDisplayName() }),
            workoutGoals  = Enum.GetValues<WorkoutGoal>() .Select(e => new { value = e.ToString(), displayName = e.GetDisplayName() }),
            focusAreas    = Enum.GetValues<FocusArea>()   .Select(e => new { value = e.ToString(), displayName = e.GetDisplayName() }),
            equipment     = Enum.GetValues<Equipment>()   .Select(e => new { value = e.ToString(), displayName = e.GetDisplayName() })
        });
    }
}
