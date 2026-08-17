using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToDo.Models;

namespace ToDo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return Ok();
        }

        public IActionResult Privacy()
        {
            return Ok();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return Ok(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ex()
        {
            throw new Exception("This is an exception"); 
        }
    }
}
 