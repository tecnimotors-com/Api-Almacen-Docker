using ApiAlmacen.Repository.ReporteRepository.Interface;
using ApiAlmacen.Repository.ReporteRepository.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiAlmacen.Controllers.Reporte
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReporteController(IReporteRepository ireporterepository) : ControllerBase
    {
        private readonly IReporteRepository _reporteRepository = ireporterepository;

        [HttpPost("RegistrarReporte")]
        public async Task<IActionResult> RegistrarReporte([FromBody] TrReporte trReporte)
        {
            if (trReporte == null)
                return BadRequest("error 404, Datos Incompletos");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _reporteRepository.RegistrarReporte(trReporte);

            return Created("create", created);
        }

        [HttpGet("ListadoReporte")]
        public async Task<IActionResult> ListadoReporte()
        {
            var result = await _reporteRepository.ListadoReporte();

            return Ok(result);
        }

        [HttpGet("ListarReporteFilter/{Codigo}")]
        public async Task<IActionResult> ListarReporteFilter(string Codigo)
        {
            var result = await _reporteRepository.ListarReporteFilter(Codigo);
            return Ok(result);
        }
    }
}
