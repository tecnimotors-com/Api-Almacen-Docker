using ApiAlmacen.Repository.AlmacenRepository.Interface;
using ApiAlmacen.Repository.AlmacenRepository.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiAlmacen.Controllers.Almacen
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AlmacenController(IAlmacenRepository almacenRepository) : ControllerBase
    {
        private readonly IAlmacenRepository ialmacenRepository = almacenRepository;

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

        [HttpGet("ListarArticuloInventarioFamilia/{Limit}/{Offset}/{Familia}")]
        public async Task<IActionResult> ListarArticuloInventarioFamilia(string Limit, string Offset, string Familia)
        {
            var result = await ialmacenRepository.ListarArticuloInventarioFamilia(Limit, Offset, Familia);

            return Ok(result);
        }

        [HttpGet("ListarArticuloSubFamilia/{Limit}/{Offset}/{Familia}/{SubFamilia}")]
        public async Task<IActionResult> ListarArticuloSubFamilia(string Limit, string Offset, string Familia, string SubFamilia)
        {
            var result = await ialmacenRepository.ListarArticuloSubFamilia(Limit, Offset, Familia, SubFamilia);

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

        [HttpGet("ListadoCountCantidad/{Articulo}/{Mes1}/{Mes2}/{Mes3}/{Mes4}/{Mes5}/{Mes6}")]
        public async Task<IActionResult> ListadoCountCantidad(string Articulo, string Mes1, string Mes2, string Mes3, string Mes4, string Mes5, string Mes6)
        {
            var result = await ialmacenRepository.ListadoCountCantidad(Articulo, Mes1, Mes2, Mes3, Mes4, Mes5, Mes6);

            return Ok(result);
        }

        [HttpGet("ListarDetalleLoteImportacion/{Limit}/{Offset}/{Articulo}")]
        public async Task<IActionResult> ListarDetalleLoteImportacion(string Limit, string Offset, string Articulo)
        {
            var result = await ialmacenRepository.ListarDetalleLoteImportacion(Limit, Offset, Articulo);

            return Ok(result);
        }

        [HttpGet("TotalDetalleLote/{Articulo}/{Tc}/{Desc}/{Igv}")]
        public async Task<IActionResult> TotalDetalleLote(string Articulo, string Tc, string Desc, float Igv)
        {
            var result = await ialmacenRepository.TotalDetalleLote(Articulo, Tc, Desc, Igv);

            return Ok(result);
        }

        [HttpGet("TotalPedidoAnalisis/{Articulo}")]
        public async Task<IActionResult> TotalPedidoAnalisis(string Articulo)
        {
            var result = await ialmacenRepository.TotalPedidoAnalisis(Articulo);

            return Ok(result);
        }
        //FilterDescripcion(string Limit, string Offset, string Descrip);
        /*------------------------------------------------------------------*/
        /*------------------------------------------------------------------*/
        /*------------------------------------------------------------------*/
        [HttpPost("FilterCodigoInterno")]
        public async Task<IActionResult> FilterCodigoInterno([FromBody] TrCodiInt trAlmacen)
        {
            if (trAlmacen == null)
                return BadRequest("error 404, Datos Incompletos");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var created = await ialmacenRepository.FilterCodigoInterno(trAlmacen.Limit!, trAlmacen.Offset!, trAlmacen.CodiInte!);

            return Created("create", created);
        }
        /*------------------------------------------------------------------*/
        [HttpPost("FilterCodigoEqui")]
        public async Task<IActionResult> FilterCodigoEqui([FromBody] TrCodiEquiFilt trAlmacen)
        {
            if (trAlmacen == null)
                return BadRequest("error 404, Datos Incompletos");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var created = await ialmacenRepository.FilterCodigoEqui(trAlmacen.Limit!, trAlmacen.Offset!, trAlmacen.CodigoEqui!);

            return Created("create", created);
        }
	[HttpGet("FilterCodigoEquiFamilia/{Limit}/{Offset}/{Familia}")]
        public async Task<IActionResult> FilterCodigoEquiFamilia(string Limit, string Offset, string Familia)
        {
            var result = await ialmacenRepository.FilterCodigoEquiFamilia(Limit, Offset, Familia);

            return Ok(result);
        }

        [HttpGet("FilterCodigoEquiSubFamilia/{Limit}/{Offset}/{Familia}/{SubFamilia}")]
        public async Task<IActionResult> FilterCodigoEquiSubFamilia(string Limit, string Offset, string Familia , string SubFamilia)
        {
            var result = await ialmacenRepository.FilterCodigoEquiSubFamilia(Limit, Offset, Familia, SubFamilia);

            return Ok(result);
        }
        /*------------------------------------------------------------------*/
        [HttpPost("FilterDescripcion")]
        public async Task<IActionResult> FilterDescripcion([FromBody] TrDescriFilt trAlmacen)
        {
            if (trAlmacen == null)
                return BadRequest("error 404, Datos Incompletos");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var created = await ialmacenRepository.FilterDescripcion(trAlmacen.Limit!, trAlmacen.Offset!, trAlmacen.Descrip!);

            return Created("create", created);
        }
	[HttpGet("FilterDescripcionFamilia/{Limit}/{Offset}/{Familia}")]
        public async Task<IActionResult> FilterDescripcionFamilia(string Limit, string Offset, string Familia)
        {
            var result = await ialmacenRepository.FilterDescripcionFamilia(Limit, Offset, Familia);

            return Ok(result);
        }
	[HttpGet("FilterDescripcionSubFamilia/{Limit}/{Offset}/{Familia}/{SubFamilia}")]
        public async Task<IActionResult> FilterDescripcionSubFamilia(string Limit, string Offset, string Familia, string SubFamilia)
        {
            var result = await ialmacenRepository.FilterDescripcionSubFamilia(Limit, Offset, Familia, SubFamilia);

            return Ok(result);
        }
	/*------------------------------------------------------------------*/
        [HttpPost("FilterProveedorfilter")]
        public async Task<IActionResult> FilterProveedorfilter([FromBody] TrFilterProve trAlmacen)
        {
            if (trAlmacen == null)
                return BadRequest("error 404, Datos Incompletos");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var created = await ialmacenRepository.FilterProveedorfilter(trAlmacen.Limit!, trAlmacen.Offset!, trAlmacen.ProveDescrip!);

            return Created("create", created);
        }
        [HttpGet("FilterProveedor/{Limit}/{Offset}")]
        public async Task<IActionResult> FilterProveedor(string Limit, string Offset)
        {
            var result = await ialmacenRepository.FilterProveedor(Limit, Offset);

            return Ok(result);
        }
	[HttpGet("FilterProveedorFamilia/{Limit}/{Offset}/{Familia}")]
        public async Task<IActionResult> FilterProveedorFamilia(string Limit, string Offset, string Familia)
        {
            var result = await ialmacenRepository.FilterProveedorFamilia(Limit, Offset, Familia);

            return Ok(result);
        }
	[HttpGet("FilterProveedorSubFamilia/{Limit}/{Offset}/{Familia}/{SubFamilia}")]
        public async Task<IActionResult> FilterProveedorSubFamilia(string Limit, string Offset, string Familia , string SubFamilia)
        {
            var result = await ialmacenRepository.FilterProveedorSubFamilia(Limit, Offset, Familia, SubFamilia);

            return Ok(result);
        }
	/*------------------------------------------------------------------*/
    }
}
