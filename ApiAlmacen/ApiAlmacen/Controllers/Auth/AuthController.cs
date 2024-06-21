using ApiAlmacen.Services.Auth;
using ApiAlmacen.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ApiAlmacen.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService serviceRepository, IConfiguration configuration) : ControllerBase
    {
        private readonly IAuthService _serviceRepository = serviceRepository;
        private readonly IConfiguration configuration = configuration;

        [HttpPost("token")]
        public IActionResult Token([FromBody] UserData data)
        {
            if (data.UserName == configuration.GetValue<string>("AuthentificactionSettings:AdminAlmacen")! &&
              data.Password == configuration.GetValue<string>("AuthentificactionSettings:Signinkey")!)
            {
                if (_serviceRepository.ValidateLogin(data.UserName, data.Password))
                {
                    var date = DateTime.UtcNow;
                    var expireDate = TimeSpan.FromHours(8);
                    var expireDateTime = date.Add(expireDate);

                    var token = _serviceRepository.GenerateToken(date, data.UserName, data.Password);
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