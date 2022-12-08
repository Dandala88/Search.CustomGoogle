using Microsoft.AspNetCore.Mvc;

namespace Search.CustomGoogle.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomGoogle : ControllerBase
    {
        private readonly ILogger<CustomGoogle> _logger;

        public CustomGoogle(ILogger<CustomGoogle> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}