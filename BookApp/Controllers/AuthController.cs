using BookApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService service) : ControllerBase
    {
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterDto user)
        {
            try
            {
                service.Register(user);
                return Ok();
            }
            catch (Exception e)
            {
                //Text som returneras,skapar JSON-objekt 
                return BadRequest(new {message = e.Message});
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDto user)
        {
            try
            {
                var token = service.Login(user);
                return Ok(new {Token = token});
            }
            catch (Exception e)
            {
                return Unauthorized(new {message = e.Message});
            }
        }
        
    }
}
