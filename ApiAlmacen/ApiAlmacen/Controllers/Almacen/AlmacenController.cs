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

        [HttpGet("ListarAnalisisCostosInventario/{Limit}/{Offset}")]
        public async Task<IActionResult> ListarAnalisisCostosInventario(string Limit, string Offset)
        {
            var result = await ialmacenRepository.ListarAnalisisCostosInventario(Limit, Offset);

            return Ok(result);
        }



        [HttpGet("ListarFamiliaCodigo")]
        public async Task<IActionResult> ListarFamiliaCodigo()
        {
            var result = await ialmacenRepository.ListarFamiliaCodigo();

            return Ok(result);
        }

        [HttpGet("ListarSubFamiliaCodigo")]
        public async Task<IActionResult> ListarSubFamiliaCodigo(string Familia)
        {
            var result = await ialmacenRepository.ListarSubFamiliaCodigo(Familia);

            return Ok(result);
        }

        [HttpGet("ListarArticuloInventario/{Limit}/{Offset}")]
        public async Task<IActionResult> ListarArticuloInventario(string Limit, string Offset)
        {
            var result = await ialmacenRepository.ListarArticuloInventario(Limit, Offset);

            return Ok(result);
        }

        [HttpGet("ListarArticuloInventarioFilter/{Limit}/{Offset}/{Articulo}")]
        public async Task<IActionResult> ListarArticuloInventarioFilter(string Limit, string Offset, string Articulo)
        {
            var result = await ialmacenRepository.ListarArticuloInventarioFilter(Limit, Offset, Articulo);

            return Ok(result);
        }
        [HttpGet("DetalleInventarioGeneral/{Articulo}")]
        public async Task<IActionResult> DetalleInventarioGeneral(string Articulo)
        {
            var result = await ialmacenRepository.DetalleInventarioGeneral(Articulo);

            return Ok(result);
        }
    }
}
