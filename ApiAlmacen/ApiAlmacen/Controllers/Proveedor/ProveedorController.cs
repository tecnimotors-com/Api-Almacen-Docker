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

        [HttpGet("ListadoProveedorAll/{Limit}/{Offset}")]
        public async Task<IActionResult> ListadoProveedorAll(int Limit, int Offset)
        {
            var result = await iproveedorrepository.ListadoProveedorAll(Limit, Offset);

            return Ok(result);
        }
        [HttpGet("ListadoProveedorCodigo/{Articulo}/{Limit}/{Offset}")]
        public async Task<IActionResult> ListadoProveedorCodigo(string Articulo, int Limit, int Offset)
        {
            var result = await iproveedorrepository.ListadoProveedorCodigo(Articulo, Limit, Offset);

            return Ok(result);
        }
        [HttpGet("ListadoProveedorFamiSubProv/{Familia}/{SubFamilia}/{Proveedor}/{Limit}/{Offset}")]
        public async Task<IActionResult> ListadoProveedorFamiSubProv(string Familia, string SubFamilia, string Proveedor, int Limit, int Offset)
        {
            var result = await iproveedorrepository.ListadoProveedorFamiSubProv(Familia, SubFamilia, Proveedor, Limit, Offset);

            return Ok(result);
        }
        /*---------------------------------------------------------------------------------------*/
        [HttpGet("ListadoNgselectProveedor")]
        public async Task<IActionResult> ListadoNgselectProveedor()
        {
            var result = await iproveedorrepository.ListadoNgselectProveedor();

            return Ok(result);
        }
        [HttpGet("ListadoNgSelectProveedorFamily/{Familia}")]
        public async Task<IActionResult> ListadoNgSelectProveedorFamily(string Familia)
        {
            var result = await iproveedorrepository.ListadoNgSelectProveedorFamily(Familia);

            return Ok(result);
        }
        [HttpGet("ListadoNgSelectProveedorSubFamily/{Familia}/{SubFamilia}")]
        public async Task<IActionResult> ListadoNgSelectProveedorSubFamily(string Familia, string SubFamilia)
        {
            var result = await iproveedorrepository.ListadoNgSelectProveedorSubFamily(Familia, SubFamilia);

            return Ok(result);
        }
        /*---------------------------------------------------------------------------------------*/
        [HttpGet("DetailFamilia/{FamiliaCode}")]
        public async Task<IActionResult> DetailFamilia(string FamiliaCode)
        {
            var result = await iproveedorrepository.DetailFamilia(FamiliaCode);

            return Ok(result);
        }
        [HttpGet("DetailSubFamilia/{FamiliaCode}/{SubFamiliaCode}")]
        public async Task<IActionResult> DetailSubFamilia(string FamiliaCode, string SubFamiliaCode)
        {
            var result = await iproveedorrepository.DetailSubFamilia(FamiliaCode, SubFamiliaCode);

            return Ok(result);
        }
        [HttpGet("DetailProveedor/{ProveedorCode}")]
        public async Task<IActionResult> DetailProveedor(string ProveedorCode)
        {
            var result = await iproveedorrepository.DetailProveedor(ProveedorCode);

            return Ok(result);
        }
    }
}
