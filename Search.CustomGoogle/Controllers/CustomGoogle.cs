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
        private readonly string googleApiKey;
        private readonly string googleCseId;

        public CustomGoogle(ILogger<CustomGoogle> logger, IConfiguration configuration)
        {
            _logger = logger;
            googleApiKey = configuration.GetSection("GoogleSearch:ApiKey").Value;
            googleCseId = configuration.GetSection("GoogleSearch:EngineId").Value;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            string query = "Omelette";

            var svc = new CustomSearchAPIService(new BaseClientService.Initializer { ApiKey = googleApiKey });
            var listRequest = svc.Cse.List();
            listRequest.Q = query;
            listRequest.Cx = googleCseId;
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