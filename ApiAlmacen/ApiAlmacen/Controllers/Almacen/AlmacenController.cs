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

        [HttpGet("ListarAnalisisCostosInventario/{Limit}/{Offset}/{Fecha_upload}")]
        public async Task<IActionResult> ListarAnalisisCostosInventario(string Limit, string Offset, string Fecha_upload)
        {
            var result1 = Fecha_upload.Replace("%2F", "/");
            var result2 = result1.Replace("%2F", "/");
            var result = await ialmacenRepository.ListarAnalisisCostosInventario(Limit, Offset, result2);

            return Ok(result);
        }

        [HttpGet("ListarFamiliaCodigo/{Fecha_upload}")]
        public async Task<IActionResult> ListarFamiliaCodigo(string Fecha_upload)
        {
            var result1 = Fecha_upload.Replace("%2F", "/");
            var result2 = result1.Replace("%2F", "/");
            var result = await ialmacenRepository.ListarFamiliaCodigo(result2);

            return Ok(result);
        }

        [HttpGet("ListarSubFamiliaCodigo/{Fecha_upload}")]
        public async Task<IActionResult> ListarSubFamiliaCodigo(string Familia, string Fecha_upload)
        {
            var result1 = Fecha_upload.Replace("%2F", "/");
            var result2 = result1.Replace("%2F", "/");
            var result = await ialmacenRepository.ListarSubFamiliaCodigo(Familia, result2);

            return Ok(result);
        }
        /*------------------------------------------------------------------------------*/
        /*------------------------------------------------------------------------------*/
        [HttpGet("ListarArticuloInventarioOnly/{Fecha_upload}")]
        public async Task<IActionResult> ListarArticuloInventarioOnly(string Fecha_upload)
        {
            var result1 = Fecha_upload.Replace("%2F", "/");
            var result2 = result1.Replace("%2F", "/");
            var result = await ialmacenRepository.ListarArticuloInventarioOnly(result2);

            return Ok(result);
        }
        [HttpGet("ListarEquivalentoInventarioOnly/{Fecha_upload}")]
        public async Task<IActionResult> ListarEquivalentoInventarioOnly(string Fecha_upload)
        {
            var result1 = Fecha_upload.Replace("%2F", "/");
            var result2 = result1.Replace("%2F", "/");
            var result = await ialmacenRepository.ListarEquivalentoInventarioOnly(result2);

            return Ok(result);
        }
        [HttpGet("ListarDescripcionInventariOnly/{Fecha_upload}")]
        public async Task<IActionResult> ListarDescripcionInventariOnly(string Fecha_upload)
        {
            var result1 = Fecha_upload.Replace("%2F", "/");
            var result2 = result1.Replace("%2F", "/");
            var result = await ialmacenRepository.ListarDescripcionInventariOnly(result2);

            return Ok(result);
        }
        /*------------------------------------------------------------------------------*/
        [HttpGet("ListarArticuloInventario/{Limit}/{Offset}/{Fecha_upload}")]
        public async Task<IActionResult> ListarArticuloInventario(string Limit, string Offset, string Fecha_upload)
        {
            var result1 = Fecha_upload.Replace("%2F", "/");
            var result2 = result1.Replace("%2F", "/");
            var result = await ialmacenRepository.ListarArticuloInventario(Limit, Offset, result2);

            return Ok(result);
        }
        [HttpGet("ListarEquivalentoInventario/{Limit}/{Offset}/{Fecha_upload}")]
        public async Task<IActionResult> ListarEquivalentoInventario(string Limit, string Offset, string Fecha_upload)
        {
            var result1 = Fecha_upload.Replace("%2F", "/");
            var result2 = result1.Replace("%2F", "/");
            var result = await ialmacenRepository.ListarEquivalentoInventario(Limit, Offset, result2);

            return Ok(result);
        }
        [HttpGet("ListarDescripcionInventario/{Limit}/{Offset}/{Fecha_upload}")]
        public async Task<IActionResult> ListarDescripcionInventario(string Limit, string Offset, string Fecha_upload)
        {
            var result1 = Fecha_upload.Replace("%2F", "/");
            var result2 = result1.Replace("%2F", "/");
            var result = await ialmacenRepository.ListarDescripcionInventario(Limit, Offset, result2);

            return Ok(result);
        }
        /*------------------------------------------------------------------------------*/
        /*------------------------------------------------------------------------------*/
        /*------------------------------------------------------------------------------*/
        [HttpGet("ListarArticuloInventarioFamilia/{Limit}/{Offset}/{Familia}/{Fecha_upload}")]
        public async Task<IActionResult> ListarArticuloInventarioFamilia(string Limit, string Offset, string Familia, string Fecha_upload)
        {
            var result1 = Fecha_upload.Replace("%2F", "/");
            var result2 = result1.Replace("%2F", "/");
            var result = await ialmacenRepository.ListarArticuloInventarioFamilia(Limit, Offset, Familia, result2);

            return Ok(result);
        }

        [HttpGet("ListarArticuloSubFamilia/{Limit}/{Offset}/{Familia}/{SubFamilia}/{Fecha_upload}")]
        public async Task<IActionResult> ListarArticuloSubFamilia(string Limit, string Offset, string Familia, string SubFamilia, string Fecha_upload)
        {
            var result1 = Fecha_upload.Replace("%2F", "/");
            var result2 = result1.Replace("%2F", "/");
            var result = await ialmacenRepository.ListarArticuloSubFamilia(Limit, Offset, Familia, SubFamilia, result2);

            return Ok(result);
        }


        [HttpGet("ListarArticuloInventarioFilter/{Limit}/{Offset}/{Articulo}/{Fecha_upload}")]
        public async Task<IActionResult> ListarArticuloInventarioFilter(string Limit, string Offset, string Articulo, string Fecha_upload)
        {
            var result1 = Fecha_upload.Replace("%2F", "/");
            var result2 = result1.Replace("%2F", "/");
            var result = await ialmacenRepository.ListarArticuloInventarioFilter(Limit, Offset, Articulo, result2);

            return Ok(result);
        }

        [HttpGet("DetalleInventarioGeneral/{Articulo}/{Fecha_upload}")]
        public async Task<IActionResult> DetalleInventarioGeneral(string Articulo, string Fecha_upload)
        {
            var result1 = Fecha_upload.Replace("%2F", "/");
            var result2 = result1.Replace("%2F", "/");
            var result = await ialmacenRepository.DetalleInventarioGeneral(Articulo, result2);

            return Ok(result);
        }

        [HttpGet("ListadoCountCantidad/{Articulo}/{Mes1}/{Mes2}/{Mes3}/{Mes4}/{Mes5}/{Mes6}")]
        public async Task<IActionResult> ListadoCountCantidad(string Articulo, string Mes1, string Mes2, string Mes3, string Mes4, string Mes5, string Mes6)
        {
            var result = await ialmacenRepository.ListadoCountCantidad(Articulo, Mes1, Mes2, Mes3, Mes4, Mes5, Mes6);

            return Ok(result);
        }

        [HttpGet("ListarDetalleLoteImportacion/{Limit}/{Offset}/{Articulo}/{Fecha_upload}")]
        public async Task<IActionResult> ListarDetalleLoteImportacion(string Limit, string Offset, string Articulo, string Fecha_upload)
        {
            var result1 = Fecha_upload.Replace("%2F", "/");
            var result2 = result1.Replace("%2F", "/");
            var result = await ialmacenRepository.ListarDetalleLoteImportacion(Limit, Offset, Articulo, result2);

            return Ok(result);
        }

        [HttpGet("ListarDetalleLoteImportacionAll/{Articulo}/{Fecha_upload}")]
        public async Task<IActionResult> ListarDetalleLoteImportacionAll(string Articulo, string Fecha_upload)
        {
            var result1 = Fecha_upload.Replace("%2F", "/");
            var result2 = result1.Replace("%2F", "/");
            var result = await ialmacenRepository.ListarDetalleLoteImportacionAll(Articulo, result2);

            return Ok(result);
        }

        [HttpGet("TotalDetalleLote/{Articulo}/{Tc}/{Desc}/{Igv}/{Fecha_upload}")]
        public async Task<IActionResult> TotalDetalleLote(string Articulo, string Tc, string Desc, float Igv, string Fecha_upload)
        {
            var result1 = Fecha_upload.Replace("%2F", "/");
            var result2 = result1.Replace("%2F", "/");
            var result = await ialmacenRepository.TotalDetalleLote(Articulo, Tc, Desc, Igv, result2);

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
            var created = await ialmacenRepository.FilterCodigoInterno(trAlmacen.Limit!, trAlmacen.Offset!, trAlmacen.CodiInte!, trAlmacen.Fecha_upload!);

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
            var created = await ialmacenRepository.FilterCodigoEqui(trAlmacen.Limit!, trAlmacen.Offset!, trAlmacen.CodigoEqui!, trAlmacen.Fecha_upload!);

            return Created("create", created);
        }
        [HttpGet("FilterCodigoEquiFamilia/{Limit}/{Offset}/{Familia}/{Fecha_upload}")]
        public async Task<IActionResult> FilterCodigoEquiFamilia(string Limit, string Offset, string Familia, string Fecha_upload)
        {
            var result1 = Fecha_upload.Replace("%2F", "/");
            var result2 = result1.Replace("%2F", "/");
            var result = await ialmacenRepository.FilterCodigoEquiFamilia(Limit, Offset, Familia, result2);

            return Ok(result);
        }

        [HttpGet("FilterCodigoEquiSubFamilia/{Limit}/{Offset}/{Familia}/{SubFamilia}/{Fecha_upload}")]
        public async Task<IActionResult> FilterCodigoEquiSubFamilia(string Limit, string Offset, string Familia, string SubFamilia, string Fecha_upload)
        {
            var result1 = Fecha_upload.Replace("%2F", "/");
            var result2 = result1.Replace("%2F", "/");
            var result = await ialmacenRepository.FilterCodigoEquiSubFamilia(Limit, Offset, Familia, SubFamilia, result2);

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
            var created = await ialmacenRepository.FilterDescripcion(trAlmacen.Limit!, trAlmacen.Offset!, trAlmacen.Descrip!, trAlmacen.Fecha_upload!);

            return Created("create", created);
        }
        [HttpGet("FilterDescripcionFamilia/{Limit}/{Offset}/{Familia}/{Fecha_upload}")]
        public async Task<IActionResult> FilterDescripcionFamilia(string Limit, string Offset, string Familia, string Fecha_upload)
        {
            var result1 = Fecha_upload.Replace("%2F", "/");
            var result2 = result1.Replace("%2F", "/");
            var result = await ialmacenRepository.FilterDescripcionFamilia(Limit, Offset, Familia, result2);

            return Ok(result);
        }
        [HttpGet("FilterDescripcionSubFamilia/{Limit}/{Offset}/{Familia}/{SubFamilia}/{Fecha_upload}")]
        public async Task<IActionResult> FilterDescripcionSubFamilia(string Limit, string Offset, string Familia, string SubFamilia, string Fecha_upload)
        {
            var result1 = Fecha_upload.Replace("%2F", "/");
            var result2 = result1.Replace("%2F", "/");
            var result = await ialmacenRepository.FilterDescripcionSubFamilia(Limit, Offset, Familia, SubFamilia, result2);

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
            var created = await ialmacenRepository.FilterProveedorfilter(trAlmacen.Limit!, trAlmacen.Offset!, trAlmacen.ProveDescrip!, trAlmacen.Fecha_upload!);

            return Created("create", created);
        }
        [HttpGet("FilterProveedor/{Limit}/{Offset}/{Fecha_upload}")]
        public async Task<IActionResult> FilterProveedor(string Limit, string Offset, string Fecha_upload)
        {
            var result1 = Fecha_upload.Replace("%2F", "/");
            var result2 = result1.Replace("%2F", "/");
            var result = await ialmacenRepository.FilterProveedor(Limit, Offset, result2);

            return Ok(result);
        }
        [HttpGet("ListadorProveedorAll/{Fecha_upload}")]
        public async Task<IActionResult> ListadorProveedorAll(string Fecha_upload)
        {
            var result1 = Fecha_upload.Replace("%2F", "/");
            var result2 = result1.Replace("%2F", "/");
            var result = await ialmacenRepository.ListadorProveedorAll(result2);

            return Ok(result);
        }
        [HttpGet("FilterProveedorFamilia/{Limit}/{Offset}/{Familia}/{Fecha_upload}")]
        public async Task<IActionResult> FilterProveedorFamilia(string Limit, string Offset, string Familia, string Fecha_upload)
        {
            var result1 = Fecha_upload.Replace("%2F", "/");
            var result2 = result1.Replace("%2F", "/");
            var result = await ialmacenRepository.FilterProveedorFamilia(Limit, Offset, Familia, result2);

            return Ok(result);
        }
        [HttpGet("FilterProveedorSubFamilia/{Limit}/{Offset}/{Familia}/{SubFamilia}/{Fecha_upload}")]
        public async Task<IActionResult> FilterProveedorSubFamilia(string Limit, string Offset, string Familia, string SubFamilia, string Fecha_upload)
        {
            var result1 = Fecha_upload.Replace("%2F", "/");
            var result2 = result1.Replace("%2F", "/");
            var result = await ialmacenRepository.FilterProveedorSubFamilia(Limit, Offset, Familia, SubFamilia, result2);

            return Ok(result);
        }
        /*------------------------------------------------------------------*/
        [HttpGet("DetalleImportacion/{Articulo}")]
        public async Task<IActionResult> DetalleImportacion(string Articulo)
        {
            var result = await ialmacenRepository.DetalleImportacion(Articulo);

            return Ok(result);
        }

        [HttpGet("ListadoFechaUpload")]
        public async Task<IActionResult> ListadoFechaUpload()
        {
            var result = await ialmacenRepository.ListadoFechaUpload();
            return Ok(result);
        }
    }
}
