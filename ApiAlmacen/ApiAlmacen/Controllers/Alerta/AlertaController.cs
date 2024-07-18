using ApiAlmacen.Repository.AlertaRepository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiAlmacen.Controllers.Alerta
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertaController(IAlertaRepository IAlertaRepository) : ControllerBase
    {
        private readonly IAlertaRepository ialertarepository = IAlertaRepository;

        [HttpGet("BusquedaListadoAlerta")]
        public async Task<IActionResult> BusquedaListadoAlerta()
        {
            var result = await ialertarepository.BusquedaListadoAlerta();
            return Ok(result);
        }

        [HttpGet("ListadoaAlertaInventario")]
        public async Task<IActionResult> ListadoaAlertaInventario()
        {
            var result = await ialertarepository.ListadoaAlertaInventario();
            return Ok(result);
        }
    }
}
