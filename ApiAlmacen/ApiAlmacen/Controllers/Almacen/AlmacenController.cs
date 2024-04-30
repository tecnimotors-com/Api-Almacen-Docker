using ApiAlmacen.Repository.AlmacenRepository.Interface;
using ApiAlmacen.Repository.AlmacenRepository.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiAlmacen.Controllers.Almacen
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AlmacenController : ControllerBase
    {
        private readonly IAlmacenRepository ialmacenRepository;
        public AlmacenController(IAlmacenRepository almacenRepository)
        {
            ialmacenRepository = almacenRepository;
        }

        [HttpPost("RegistrarAlmacen")]
        public async Task<IActionResult> RegistrarAlmacen([FromBody] TrAlmacen trAlmacen)
        {
            if (trAlmacen == null)
                return BadRequest("error 404, Datos Incompletos");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var created = await ialmacenRepository.RegistrarAlmacen(trAlmacen);

            return Created("create", created);
        }

        [HttpGet("ListarProductoAlmacen/{Articulo}")]
        public async Task<IActionResult> ListarProductoAlmacen(string Articulo)
        {
            var result = await ialmacenRepository.ListarProductoAlmacen(Articulo);

            return Ok(new { result, code = 200, estado = "lleno", msg = "ok 200 , tabla llena" });
        }

        [HttpGet("ListarProductoCompletoAlmacen")]
        public async Task<IActionResult> ListarProductoCompletoAlmacen(string Articulo)
        {
            var result = await ialmacenRepository.ListarProductoCompletoAlmacen(Articulo);

            return Ok(result);
        }

        [HttpGet("ListarProductoCompletoAlmacen2")]
        public async Task<IActionResult> ListarProductoCompletoAlmacen2()
        {
            var result = await ialmacenRepository.ListarProductoCompletoAlmacen2();

            return Ok(result);
        }


    }
}
