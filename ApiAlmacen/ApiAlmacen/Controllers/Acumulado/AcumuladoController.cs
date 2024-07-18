using ApiAlmacen.Repository.AcumuladoRepository.Interface;
using ApiAlmacen.Repository.AcumuladoRepository.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiAlmacen.Controllers.Acumulado
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AcumuladoController(IAcumuladoRepository acumuladorepository) : ControllerBase
    {
        private readonly IAcumuladoRepository _acumuladoRepository = acumuladorepository;

        [HttpPost("ListGroupCodigo/{Fechaacumulado}/{Nombrepedido}/{Cantidadpedido}/{Nombreviajando}/{Cantidadviajando}")]
        public async Task<IActionResult> ListGroupCodigo([FromBody] List<string> data, string Fechaacumulado, string Nombrepedido,
            string Cantidadpedido, string Nombreviajando, string Cantidadviajando)
        {
            // Procesar los datos recibidos
            foreach (var item in data)
            {
                var result = await _acumuladoRepository.FiltroCodigoAcumulado(item);

                if (result == null)
                {
                    TrModels model = new()
                    {
                        Codigo = item,
                        Fechaacumulado = Fechaacumulado,
                        Nombrepedido = Nombrepedido,
                        Cantidadpedido = Cantidadpedido,
                        Nombreviajando = Nombreviajando,
                        Cantidadviajando = Cantidadviajando
                    };

                    await _acumuladoRepository.RegistrarAcumulados(model);
                    Thread.Sleep(100);
                }
                else
                {
                    TaAcumulado model = new()
                    {
                        Codigo = item,
                        Fechaacumulado = Fechaacumulado,
                        Nombrepedido = Nombrepedido,
                        Cantidadpedido = Cantidadpedido,
                        Nombreviajando = Nombreviajando,
                        Cantidadviajando = Cantidadviajando
                    };

                    await _acumuladoRepository.ActualizarAcumulado(model);
                    Thread.Sleep(100);
                }
            }
            return Ok(new { Message = "Datos recibidos correctamente" });
        }
    }
}
