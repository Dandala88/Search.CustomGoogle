using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Google.Apis.CustomSearchAPI.v1;
using Google.Apis.Services;
using System.Text.Json;
using Search.CustomGoogle.Models;
using Search.CustomGoogle.Interfaces;

namespace Search.CustomGoogle.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomGoogle : ControllerBase
    {
        private readonly ILogger<CustomGoogle> _logger;
        private readonly ICustomGoogleService _customGoogleService;

        public CustomGoogle(ILogger<CustomGoogle> logger, IConfiguration configuration, ICustomGoogleService customGoogleService)
        {
            _logger = logger;
            _customGoogleService = customGoogleService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get([FromQuery] string q)
        {
            try
            {
                var result = _customGoogleService.Search(q);

                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("Pages")]
        public async Task<IActionResult> GetPages([FromQuery] string q)
        {
            try
            {
                var result = _customGoogleService.Search(q);
                var pages = await _customGoogleService.GetPages(result);

                return Ok(pages);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}