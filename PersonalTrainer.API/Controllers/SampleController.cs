using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace PersonalTrainer.API.Controllers
{
    [ApiController] 
    [Route("api/[controller]")]
    public class SampleController : ControllerBase
    {
        public SampleController() { }

        public IActionResult TestMethod()
        {
            return Ok();
        }
    }
}