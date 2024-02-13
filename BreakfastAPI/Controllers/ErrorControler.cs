using Microsoft.AspNetCore.Mvc;

namespace BreakfastAPI.Controllers
{
    
    public class ErrorControler: ControllerBase
    {
        [Route("/error")]
        [HttpGet]
        public IActionResult Error()
        {
            return Problem();
        }
    }
}

