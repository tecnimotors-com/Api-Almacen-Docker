using ApiAlmacen.Services.Auth;
using ApiAlmacen.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ApiAlmacen.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _serviceRepository;
        public AuthController(IAuthService serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }
        [HttpPost("token")]
        public IActionResult Token([FromBody] UserData data)
        {
            if (data.UserName == "Admin" && data.Password == "asdjnijoqnfenfiwnqdnaspmopdbdyiwqbyuaiwqieuqybAlmacen")
            {
                if (_serviceRepository.ValidateLogin(data.UserName, data.Password))
                {
                    var date = DateTime.UtcNow;
                    var expireDate = TimeSpan.FromHours(8);
                    var expireDateTime = date.Add(expireDate);

                    var token = _serviceRepository.GenerateToken(date, data.UserName, expireDate, data.Password);
                    return Ok(new
                    {
                        tokenAlmacen = token,
                        ExpireAt = expireDateTime
                    });
                }
                else
                {
                    return StatusCode(401);
                }
            }
            else
            {
                return Ok(new
                {
                    mensaje = "no tienes permiso de entrar"
                });
            }
        }
    }
}