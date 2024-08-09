using ApiAlmacen.Repository.ProveedorRepository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiAlmacen.Controllers.Proveedor
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProveedorController(IProveedorRepository proveedorrepository) : ControllerBase
    {
        private readonly IProveedorRepository iproveedorrepository = proveedorrepository;

        [HttpGet("ListadoProveedorAll/{Limit}/{Offset}/{Fecha_upload}")]
        public async Task<IActionResult> ListadoProveedorAll(int Limit, int Offset, string Fecha_upload)
        {
            var result1 = Fecha_upload.Replace("%2F", "/");
            var result2 = result1.Replace("%2F", "/");
            var result = await iproveedorrepository.ListadoProveedorAll(Limit, Offset, result2);

            return Ok(result);
        }
        [HttpGet("ListadoProveedorCodigo/{Articulo}/{Limit}/{Offset}/{Fecha_upload}")]
        public async Task<IActionResult> ListadoProveedorCodigo(string Articulo, int Limit, int Offset, string Fecha_upload)
        {
            var result1 = Fecha_upload.Replace("%2F", "/");
            var result2 = result1.Replace("%2F", "/");
            var result = await iproveedorrepository.ListadoProveedorCodigo(Articulo, Limit, Offset, result2);

            return Ok(result);
        }
        [HttpGet("ListadoProveedorFamiSubProv/{Familia}/{SubFamilia}/{Proveedor}/{Limit}/{Offset}/{Fecha_upload}")]
        public async Task<IActionResult> ListadoProveedorFamiSubProv(string Familia, string SubFamilia, string Proveedor, int Limit, int Offset, string Fecha_upload)
        {
            var result1 = Fecha_upload.Replace("%2F", "/");
            var result2 = result1.Replace("%2F", "/");
            var result = await iproveedorrepository.ListadoProveedorFamiSubProv(Familia, SubFamilia, Proveedor, Limit, Offset, result2);

            return Ok(result);
        }
        /*---------------------------------------------------------------------------------------*/
        [HttpGet("ListadoNgselectProveedor/{Fecha_upload}")]
        public async Task<IActionResult> ListadoNgselectProveedor(string Fecha_upload)
        {
            var result1 = Fecha_upload.Replace("%2F", "/");
            var result2 = result1.Replace("%2F", "/");
            var result = await iproveedorrepository.ListadoNgselectProveedor(result2);

            return Ok(result);
        }
        [HttpGet("ListadoNgSelectProveedorFamily/{Familia}/{Fecha_upload}")]
        public async Task<IActionResult> ListadoNgSelectProveedorFamily(string Familia,string Fecha_upload)
        {
            var result1 = Fecha_upload.Replace("%2F", "/");
            var result2 = result1.Replace("%2F", "/");
            var result = await iproveedorrepository.ListadoNgSelectProveedorFamily(Familia, result2);

            return Ok(result);
        }
        [HttpGet("ListadoNgSelectProveedorSubFamily/{Familia}/{SubFamilia}/{Fecha_upload}")]
        public async Task<IActionResult> ListadoNgSelectProveedorSubFamily(string Familia, string SubFamilia, string Fecha_upload)
        {
            var result1 = Fecha_upload.Replace("%2F", "/");
            var result2 = result1.Replace("%2F", "/");
            var result = await iproveedorrepository.ListadoNgSelectProveedorSubFamily(Familia, SubFamilia, result2);

            return Ok(result);
        }
        /*---------------------------------------------------------------------------------------*/
        [HttpGet("DetailFamilia/{FamiliaCode}/{Fecha_upload}")]
        public async Task<IActionResult> DetailFamilia(string FamiliaCode,string Fecha_upload)
        {
            var result1 = Fecha_upload.Replace("%2F", "/");
            var result2 = result1.Replace("%2F", "/");
            var result = await iproveedorrepository.DetailFamilia(FamiliaCode, result2);

            return Ok(result);
        }
        [HttpGet("DetailSubFamilia/{FamiliaCode}/{SubFamiliaCode}/{Fecha_upload}")]
        public async Task<IActionResult> DetailSubFamilia(string FamiliaCode, string SubFamiliaCode, string Fecha_upload)
        {
            var result1 = Fecha_upload.Replace("%2F", "/");
            var result2 = result1.Replace("%2F", "/");
            var result = await iproveedorrepository.DetailSubFamilia(FamiliaCode, SubFamiliaCode, result2);

            return Ok(result);
        }
        [HttpGet("DetailProveedor/{ProveedorCode}/{Fecha_upload}")]
        public async Task<IActionResult> DetailProveedor(string ProveedorCode,string Fecha_upload)
        {
            var result1 = Fecha_upload.Replace("%2F", "/");
            var result2 = result1.Replace("%2F", "/");
            var result = await iproveedorrepository.DetailProveedor(ProveedorCode, result2);

            return Ok(result);
        }
    }
}
