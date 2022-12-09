using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Google.Apis.CustomSearchAPI.v1;
using Google.Apis.Services;

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
        [Authorize]
        public IActionResult Get()
        {
            string apiKey = " AIzaSyAN6bAeMhdBgrToaMfpT6AA8YWJplAvi4Y ";
            string cx = "223af247142d64b1b";
            string query = "Omelette";

            var svc = new CustomSearchAPIService(new BaseClientService.Initializer { ApiKey = apiKey });
            var listRequest = svc.Cse.List();
            listRequest.Q = query;
            listRequest.Cx = cx;
            try
            {
                var result = listRequest.Execute();

                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}