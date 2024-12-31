using Microsoft.AspNetCore.Mvc;
using MinimalAPI_Project.JWTUtilites;
using MinimalAPI_Project.Models;

namespace MinimalAPI_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController(JwtTokenGenerator jwtTokenGenerator) : Controller
    {
        private readonly JwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            if (login.Password == "123456" && login.Username == "admin")//for testing issues
            {
                var token = _jwtTokenGenerator.GenerateToken(login.Username);
                return Ok(new { Token = token});
            }
            return Unauthorized("Creadentials are invalid");
        }

    }
}
