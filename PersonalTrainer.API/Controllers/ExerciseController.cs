using Microsoft.AspNetCore.Mvc;
using PersonalTrainer.API.Enums;
using PersonalTrainer.API.Services;

namespace PersonalTrainer.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseQueryService _exerciseService;
        public ExerciseController(IExerciseQueryService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        [HttpGet("Query")]
        public async Task<IActionResult> Query([FromQuery] FocusArea focusArea, [FromQuery] List<Equipment> equipment)
        {

            var exercises = await _exerciseService.GetExercisesForProfileAsync(focusArea, equipment);

           return Ok(new
        {
            count = exercises.Count,
            exercises
        });
        }
    }
}
