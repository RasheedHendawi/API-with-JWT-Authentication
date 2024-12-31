using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MinimalAPI_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : Controller
    {
        [HttpGet("public")]
        public IActionResult GetPublicData()
        {
            return Ok("This is a public/free endpoint that is accessible without authentication.");
        }

        [Authorize]
        [HttpGet("weather")]
        public IActionResult GetPrivateData()
        {
            var tmpWeatherData = new
            {
                Data = DateTime.Now,
                TempratureC = 25,
                Summary = "Looks Sunny"
            };
            return Ok(tmpWeatherData);
        }
    }
}
