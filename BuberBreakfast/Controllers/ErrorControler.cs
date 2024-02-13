using Microsoft.AspNetCore.Mvc;

namespace BuberBreakfast.Controllers
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

