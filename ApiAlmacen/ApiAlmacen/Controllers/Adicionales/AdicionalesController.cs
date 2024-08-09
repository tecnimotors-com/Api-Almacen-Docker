using ApiAlmacen.Repository.AcumuladoRepository.Interface;
using ApiAlmacen.Repository.AdicionalesRepository.Interface;
using ApiAlmacen.Repository.AlmacenRepository.Interface;
using ApiAlmacen.Repository.AlmacenRepository.Models;
using ApiAlmacen.Repository.ProveedorRepository.Interface;
using ApiAlmacen.Repository.ProveedorRepository.Models;
using ApiAlmacen.Repository.ReporteRepository.Interface;
using ApiAlmacen.Repository.ReporteRepository.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiAlmacen.Controllers.Adicionales
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdicionalesController(
        IAlmacenRepository IAlmacenrepository,
        IAcumuladoRepository IAcumuladorepository,
        IProveedorRepository IProveedorrepository,
        IReporteRepository IReporterepository,
        IAdicionalesRepository IAdicionalesrepository
        ) : ControllerBase
    {
        private readonly IAlmacenRepository ialmacenrepository = IAlmacenrepository;
        private readonly IAcumuladoRepository iacumuladorepository = IAcumuladorepository;
        private readonly IProveedorRepository iproveedorrepository = IProveedorrepository;
        private readonly IReporteRepository ireporterepository = IReporterepository;
        private readonly IAdicionalesRepository iadicionalesrepository = IAdicionalesrepository;

        private static string mont1 = "";
        private static string mont2 = "";
        private static string mont3 = "";
        private static string mont4 = "";
        private static string mont5 = "";
        private static string mont6 = "";

        private static string montcant1 = "";
        private static string montcant2 = "";
        private static string montcant3 = "";
        private static string montcant4 = "";
        private static string montcant5 = "";
        private static string montcant6 = "";

        private readonly List<TipoAr> arrayTipoArticulo = [
            new() { TipoArticulo = "Codigo Interno" },
            new() { TipoArticulo = "Codigo Equivalente" },
            new() { TipoArticulo = "Descripcion" },
        ];
        // "Nacional", "Extranjero"
        private readonly List<TipoPro> arrayTipoProve = [
            new() { TipoProve = "Nacional" },
            new() { TipoProve = "Extranjero" }
        ];

        private readonly List<Meses> arrayMeses = [
            new() { Month = "Enero" },
            new() { Month = "Febrero" },
            new() { Month = "Marzo" },
            new() { Month = "Abril" },
            new() { Month = "Mayo" },
            new() { Month = "Junio" },
            new() { Month = "Julio" },
            new() { Month = "Agosto" },
            new() { Month = "Septiembre" },
            new() { Month = "Octubre" },
            new() { Month = "Noviembre" },
            new() { Month = "Diciembre" },
        ];

        [HttpGet("ListarAllIniciales/{Fecha_upload}")]
        public async Task<IActionResult> ListarAllIniciales(string Fecha_upload)
        {
            var result1 = Fecha_upload.Replace("%2F", "/");
            var result2 = result1.Replace("%2F", "/");
            var Mes = DateTime.Now.ToString("MM");
            var Anio = DateTime.Now.ToString("yyyy");

            Meses(Mes, Anio);

            int Limit = 250;
            int Offset = 0;
            var arrayfamily = await ialmacenrepository.ListarFamiliaCodigo(result2);
            var arrayproveedor = await iproveedorrepository.ListadoNgselectProveedor(result2);
            var arraycodigointerno = await iproveedorrepository.ListadoProveedorAll(Limit, Offset, result2);
            Thread.Sleep(100);


            TlAllData tldata = new()
            {
                Month1 = mont1,
                Month2 = mont2,
                Month3 = mont3,
                Month4 = mont4,
                Month5 = mont5,
                Month6 = mont6,
                ListTipoArti = arrayTipoArticulo,
                ListTipoProve = arrayTipoProve,
                ListMeses = arrayMeses,
                ListFamily = (List<TlFamilia>)arrayfamily,
                Listproveedor = (List<TlngselectProve>)arrayproveedor,
                ListCodigoInterno = (List<TlProveedor>)arraycodigointerno
            };
            return Ok(tldata);
        }
        [HttpPost("ListarSearchCodigo")]
        public async Task<IActionResult> ListarSearchCodigo([FromBody] TlSearchCodigo tlsearch)
        {
            var result1 = tlsearch.Fechaupload!.Replace("%2F", "/");
            var result2 = result1.Replace("%2F", "/");
            if (tlsearch.Tipoarticulo == "Codigo Interno")
            {
                if (tlsearch.Familia == "" && tlsearch.Subfamilia == "" && tlsearch.Proveedor == "")
                {
                    var arraylist = await iadicionalesrepository.ListarAnalisis(
                        tlsearch.Limit,
                        tlsearch.Offset,
                        result2);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaCodigo(result2);
                    var arrayproveedor = await iproveedorrepository.ListadoNgselectProveedor(result2);
                    Thread.Sleep(100);

                    SearchData searchData = new()
                    {
                        ListCodigoInterno = (List<TlProveedor>)arraylist,
                        ListFamilia = (List<TlFamilia>)arrayfamily,
                        ListSubFamilia = [],
                        ListProveedor = (List<TlngselectProve>)arrayproveedor,
                    };
                    return Ok(searchData);
                }
                else if (tlsearch.Familia != "" && tlsearch.Subfamilia == "" && tlsearch.Proveedor == "")
                {
                    var arraylist = await iadicionalesrepository.ListarAnalisisFamilia(
                        tlsearch.Limit,
                        tlsearch.Offset,
                        tlsearch.Familia!,
                        result2);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaCodigo(result2);
                    var arraysubfamily = await ialmacenrepository.ListarSubFamiliaCodigo(tlsearch.Familia!, result2);
                    var arrayproveedor = await iproveedorrepository.ListadoNgSelectProveedorFamily(tlsearch.Familia!, result2);
                    Thread.Sleep(100);

                    SearchData searchData = new()
                    {
                        ListCodigoInterno = (List<TlProveedor>)arraylist,
                        ListFamilia = (List<TlFamilia>)arrayfamily,
                        ListSubFamilia = (List<TlSubFamilia>)arraysubfamily,
                        ListProveedor = (List<TlngselectProve>)arrayproveedor,
                    };
                    return Ok(searchData);
                }
                else if (tlsearch.Familia != "" && tlsearch.Subfamilia != "" && tlsearch.Proveedor == "")
                {
                    var arraylist = await iadicionalesrepository.ListarAnalisisFamiliaSubFamilia(
                        tlsearch.Limit,
                        tlsearch.Offset,
                        tlsearch.Familia!,
                        tlsearch.Subfamilia!,
                        result2);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaCodigo(result2);
                    var arraysubfamily = await ialmacenrepository.ListarSubFamiliaCodigo(tlsearch.Familia!, result2);
                    var arrayproveedor = await iproveedorrepository.ListadoNgSelectProveedorSubFamily(tlsearch.Familia!, tlsearch.Subfamilia!, result2);
                    Thread.Sleep(100);

                    SearchData searchData = new()
                    {
                        ListCodigoInterno = (List<TlProveedor>)arraylist,
                        ListFamilia = (List<TlFamilia>)arrayfamily,
                        ListSubFamilia = (List<TlSubFamilia>)arraysubfamily,
                        ListProveedor = (List<TlngselectProve>)arrayproveedor,
                    };
                    return Ok(searchData);
                }
                else if (tlsearch.Familia != "" && tlsearch.Subfamilia != "" && tlsearch.Proveedor != "")
                {
                    var arraylist = await iadicionalesrepository.ListarAnalisisFamiliaSubFamiliaProveedor(
                       tlsearch.Limit,
                       tlsearch.Offset,
                       tlsearch.Familia!,
                       tlsearch.Subfamilia!,
                       tlsearch.Proveedor!,
                       result2);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaCodigo(result2);
                    var arraysubfamily = await ialmacenrepository.ListarSubFamiliaCodigo(tlsearch.Familia!, result2);
                    var arrayproveedor = await iproveedorrepository.ListadoNgSelectProveedorSubFamily(tlsearch.Familia!, tlsearch.Subfamilia!, result2);
                    Thread.Sleep(100);

                    SearchData searchData = new()
                    {
                        ListCodigoInterno = (List<TlProveedor>)arraylist,
                        ListFamilia = (List<TlFamilia>)arrayfamily,
                        ListSubFamilia = (List<TlSubFamilia>)arraysubfamily,
                        ListProveedor = (List<TlngselectProve>)arrayproveedor,
                    };
                    return Ok(searchData);
                }
                /*----------------------------------------------------------------------------------*/
                /*----------------------------------------------------------------------------------*/
                /*----------------------------------------------------------------------------------*/
                /*----------------------------------------------------------------------------------*/
                /*----------------------------------------------------------------------------------*/
                else if (tlsearch.Familia == "" && tlsearch.Subfamilia != "" && tlsearch.Proveedor == "")
                {
                    var arraylist = await iadicionalesrepository.ListarAnalisis(
                       tlsearch.Limit,
                       tlsearch.Offset,
                       result2);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaCodigo(result2);
                    var arrayproveedor = await iproveedorrepository.ListadoNgselectProveedor(result2);
                    Thread.Sleep(100);

                    SearchData searchData = new()
                    {
                        ListCodigoInterno = (List<TlProveedor>)arraylist,
                        ListFamilia = (List<TlFamilia>)arrayfamily,
                        ListSubFamilia = [],
                        ListProveedor = (List<TlngselectProve>)arrayproveedor,
                    };
                    return Ok(searchData);
                }
                else if (tlsearch.Familia == "" && tlsearch.Subfamilia == "" && tlsearch.Proveedor != "")
                {
                    var arraylist = await iadicionalesrepository.ListarAnalisisProveedor(
                       tlsearch.Limit,
                       tlsearch.Offset,
                       tlsearch.Proveedor!,
                       result2);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaProveedor(tlsearch.Proveedor!, result2);
                    var arrayproveedor = await iproveedorrepository.ListadoNgselectProveedor(result2);
                    Thread.Sleep(100);

                    SearchData searchData = new()
                    {
                        ListCodigoInterno = (List<TlProveedor>)arraylist,
                        ListFamilia = (List<TlFamilia>)arrayfamily,
                        ListSubFamilia = [],
                        ListProveedor = (List<TlngselectProve>)arrayproveedor,
                    };
                    return Ok(searchData);
                }
                else if (tlsearch.Familia != "" && tlsearch.Subfamilia == "" && tlsearch.Proveedor != "")
                {
                    var arraylist = await iadicionalesrepository.ListarAnalisisFamiliaProveedor(
                     tlsearch.Limit,
                     tlsearch.Offset,
                     tlsearch.Familia!,
                     tlsearch.Proveedor!,
                     result2);
                    var arrayproveedor = await iproveedorrepository.ListadoNgSelectProveedorFamily(tlsearch.Familia!, result2);
                    var arraysubfamily = await ialmacenrepository.ListarSubFamiliaCodigo(tlsearch.Familia!, result2);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaProveedor(tlsearch.Proveedor!, result2);
                    Thread.Sleep(100);

                    SearchData searchData = new()
                    {
                        ListCodigoInterno = (List<TlProveedor>)arraylist,
                        ListFamilia = (List<TlFamilia>)arrayfamily,
                        ListSubFamilia = (List<TlSubFamilia>)arraysubfamily,
                        ListProveedor = (List<TlngselectProve>)arrayproveedor,
                    };
                    return Ok(searchData);
                }
                else if (tlsearch.Familia == "" && tlsearch.Subfamilia != "" && tlsearch.Proveedor != "")
                {
                    var arraylist = await iadicionalesrepository.ListarAnalisisProveedor(
                       tlsearch.Limit,
                       tlsearch.Offset,
                       tlsearch.Proveedor!,
                       result2);
                    var arrayproveedor = await iproveedorrepository.ListadoNgselectProveedor(result2);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaCodigo(result2);
                    Thread.Sleep(100);

                    SearchData searchData = new()
                    {
                        ListCodigoInterno = (List<TlProveedor>)arraylist,
                        ListFamilia = (List<TlFamilia>)arrayfamily,
                        ListSubFamilia = [],
                        ListProveedor = (List<TlngselectProve>)arrayproveedor,
                    };
                    return Ok(searchData);
                }
                else
                {
                    var arraylist = await iadicionalesrepository.ListarAnalisisProveedor(
                     tlsearch.Limit,
                     tlsearch.Offset,
                     tlsearch.Proveedor!,
                     result2);
                    var arrayproveedor = await iproveedorrepository.ListadoNgselectProveedor(result2);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaCodigo(result2);
                    Thread.Sleep(100);

                    SearchData searchData = new()
                    {
                        ListCodigoInterno = (List<TlProveedor>)arraylist,
                        ListFamilia = (List<TlFamilia>)arrayfamily,
                        ListSubFamilia = [],
                        ListProveedor = (List<TlngselectProve>)arrayproveedor,
                    };
                    return Ok(searchData);
                }
            }
            else if (tlsearch.Tipoarticulo == "Codigo Equivalente")
            {
                if (tlsearch.Familia == "" && tlsearch.Subfamilia == "" && tlsearch.Proveedor == "")
                {
                    var arraylist = await iadicionalesrepository.ListarAnalisis(
                        tlsearch.Limit,
                        tlsearch.Offset,
                        result2);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaCodigo(result2);
                    var arrayproveedor = await iproveedorrepository.ListadoNgselectProveedor(result2);
                    Thread.Sleep(100);

                    SearchData searchData = new()
                    {
                        ListCodigoInterno = (List<TlProveedor>)arraylist,
                        ListFamilia = (List<TlFamilia>)arrayfamily,
                        ListSubFamilia = [],
                        ListProveedor = (List<TlngselectProve>)arrayproveedor,
                    };
                    return Ok(searchData);
                }
                else if (tlsearch.Familia != "" && tlsearch.Subfamilia == "" && tlsearch.Proveedor == "")
                {
                    var arraylist = await iadicionalesrepository.ListarAnalisisFamilia(
                        tlsearch.Limit,
                        tlsearch.Offset,
                        tlsearch.Familia!,
                        result2);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaCodigo(result2);
                    var arraysubfamily = await ialmacenrepository.ListarSubFamiliaCodigo(tlsearch.Familia!, result2);
                    var arrayproveedor = await iproveedorrepository.ListadoNgSelectProveedorFamily(tlsearch.Familia!, result2);
                    Thread.Sleep(100);

                    SearchData searchData = new()
                    {
                        ListCodigoInterno = (List<TlProveedor>)arraylist,
                        ListFamilia = (List<TlFamilia>)arrayfamily,
                        ListSubFamilia = (List<TlSubFamilia>)arraysubfamily,
                        ListProveedor = (List<TlngselectProve>)arrayproveedor,
                    };
                    return Ok(searchData);
                }
                else if (tlsearch.Familia != "" && tlsearch.Subfamilia != "" && tlsearch.Proveedor == "")
                {
                    var arraylist = await iadicionalesrepository.ListarAnalisisFamiliaSubFamilia(
                        tlsearch.Limit,
                        tlsearch.Offset,
                        tlsearch.Familia!,
                        tlsearch.Subfamilia!,
                        result2);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaCodigo(result2);
                    var arraysubfamily = await ialmacenrepository.ListarSubFamiliaCodigo(tlsearch.Familia!, result2);
                    var arrayproveedor = await iproveedorrepository.ListadoNgSelectProveedorSubFamily(tlsearch.Familia!, tlsearch.Subfamilia!, result2);
                    Thread.Sleep(100);

                    SearchData searchData = new()
                    {
                        ListCodigoInterno = (List<TlProveedor>)arraylist,
                        ListFamilia = (List<TlFamilia>)arrayfamily,
                        ListSubFamilia = (List<TlSubFamilia>)arraysubfamily,
                        ListProveedor = (List<TlngselectProve>)arrayproveedor,
                    };
                    return Ok(searchData);
                }
                else if (tlsearch.Familia != "" && tlsearch.Subfamilia != "" && tlsearch.Proveedor != "")
                {
                    var arraylist = await iadicionalesrepository.ListarAnalisisFamiliaSubFamiliaProveedor(
                       tlsearch.Limit,
                       tlsearch.Offset,
                       tlsearch.Familia!,
                       tlsearch.Subfamilia!,
                       tlsearch.Proveedor!,
                       result2);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaCodigo(result2);
                    var arraysubfamily = await ialmacenrepository.ListarSubFamiliaCodigo(tlsearch.Familia!, result2);
                    var arrayproveedor = await iproveedorrepository.ListadoNgSelectProveedorSubFamily(tlsearch.Familia!, tlsearch.Subfamilia!, result2);
                    Thread.Sleep(100);

                    SearchData searchData = new()
                    {
                        ListCodigoInterno = (List<TlProveedor>)arraylist,
                        ListFamilia = (List<TlFamilia>)arrayfamily,
                        ListSubFamilia = (List<TlSubFamilia>)arraysubfamily,
                        ListProveedor = (List<TlngselectProve>)arrayproveedor,
                    };
                    return Ok(searchData);
                }
                /*----------------------------------------------------------------------------------*/
                /*----------------------------------------------------------------------------------*/
                /*----------------------------------------------------------------------------------*/
                /*----------------------------------------------------------------------------------*/
                /*----------------------------------------------------------------------------------*/
                else if (tlsearch.Familia == "" && tlsearch.Subfamilia != "" && tlsearch.Proveedor == "")
                {
                    var arraylist = await iadicionalesrepository.ListarAnalisis(
                       tlsearch.Limit,
                       tlsearch.Offset,
                       result2);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaCodigo(result2);
                    var arrayproveedor = await iproveedorrepository.ListadoNgselectProveedor(result2);
                    Thread.Sleep(100);

                    SearchData searchData = new()
                    {
                        ListCodigoInterno = (List<TlProveedor>)arraylist,
                        ListFamilia = (List<TlFamilia>)arrayfamily,
                        ListSubFamilia = [],
                        ListProveedor = (List<TlngselectProve>)arrayproveedor,
                    };
                    return Ok(searchData);
                }
                else if (tlsearch.Familia == "" && tlsearch.Subfamilia == "" && tlsearch.Proveedor != "")
                {
                    var arraylist = await iadicionalesrepository.ListarAnalisisProveedor(
                       tlsearch.Limit,
                       tlsearch.Offset,
                       tlsearch.Proveedor!,
                       result2);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaProveedor(tlsearch.Proveedor!, result2);
                    var arrayproveedor = await iproveedorrepository.ListadoNgselectProveedor(result2);
                    Thread.Sleep(100);

                    SearchData searchData = new()
                    {
                        ListCodigoInterno = (List<TlProveedor>)arraylist,
                        ListFamilia = (List<TlFamilia>)arrayfamily,
                        ListSubFamilia = [],
                        ListProveedor = (List<TlngselectProve>)arrayproveedor,
                    };
                    return Ok(searchData);
                }
                else if (tlsearch.Familia != "" && tlsearch.Subfamilia == "" && tlsearch.Proveedor != "")
                {
                    var arraylist = await iadicionalesrepository.ListarAnalisisFamiliaProveedor(
                     tlsearch.Limit,
                     tlsearch.Offset,
                     tlsearch.Familia!,
                     tlsearch.Proveedor!,
                     result2);
                    var arrayproveedor = await iproveedorrepository.ListadoNgSelectProveedorFamily(tlsearch.Familia!, result2);
                    var arraysubfamily = await ialmacenrepository.ListarSubFamiliaCodigo(tlsearch.Familia!, result2);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaProveedor(tlsearch.Proveedor!, result2);
                    Thread.Sleep(100);

                    SearchData searchData = new()
                    {
                        ListCodigoInterno = (List<TlProveedor>)arraylist,
                        ListFamilia = (List<TlFamilia>)arrayfamily,
                        ListSubFamilia = (List<TlSubFamilia>)arraysubfamily,
                        ListProveedor = (List<TlngselectProve>)arrayproveedor,
                    };
                    return Ok(searchData);
                }
                else if (tlsearch.Familia == "" && tlsearch.Subfamilia != "" && tlsearch.Proveedor != "")
                {
                    var arraylist = await iadicionalesrepository.ListarAnalisisProveedor(
                       tlsearch.Limit,
                       tlsearch.Offset,
                       tlsearch.Proveedor!,
                       result2);
                    var arrayproveedor = await iproveedorrepository.ListadoNgselectProveedor(result2);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaCodigo(result2);
                    Thread.Sleep(100);

                    SearchData searchData = new()
                    {
                        ListCodigoInterno = (List<TlProveedor>)arraylist,
                        ListFamilia = (List<TlFamilia>)arrayfamily,
                        ListSubFamilia = [],
                        ListProveedor = (List<TlngselectProve>)arrayproveedor,
                    };
                    return Ok(searchData);
                }
                else
                {
                    var arraylist = await iadicionalesrepository.ListarAnalisisProveedor(
                     tlsearch.Limit,
                     tlsearch.Offset,
                     tlsearch.Proveedor!,
                     result2);
                    var arrayproveedor = await iproveedorrepository.ListadoNgselectProveedor(result2);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaCodigo(result2);
                    Thread.Sleep(100);

                    SearchData searchData = new()
                    {
                        ListCodigoInterno = (List<TlProveedor>)arraylist,
                        ListFamilia = (List<TlFamilia>)arrayfamily,
                        ListSubFamilia = [],
                        ListProveedor = (List<TlngselectProve>)arrayproveedor,
                    };
                    return Ok(searchData);
                }
            }
            else if (tlsearch.Tipoarticulo == "Descripcion")
            {
                if (tlsearch.Familia == "" && tlsearch.Subfamilia == "" && tlsearch.Proveedor == "")
                {
                    var arraylist = await iadicionalesrepository.ListarAnalisis(
                        tlsearch.Limit,
                        tlsearch.Offset,
                        result2);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaCodigo(result2);
                    var arrayproveedor = await iproveedorrepository.ListadoNgselectProveedor(result2);
                    Thread.Sleep(100);

                    SearchData searchData = new()
                    {
                        ListCodigoInterno = (List<TlProveedor>)arraylist,
                        ListFamilia = (List<TlFamilia>)arrayfamily,
                        ListSubFamilia = [],
                        ListProveedor = (List<TlngselectProve>)arrayproveedor,
                    };
                    return Ok(searchData);
                }
                else if (tlsearch.Familia != "" && tlsearch.Subfamilia == "" && tlsearch.Proveedor == "")
                {
                    var arraylist = await iadicionalesrepository.ListarAnalisisFamilia(
                        tlsearch.Limit,
                        tlsearch.Offset,
                        tlsearch.Familia!,
                        result2);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaCodigo(result2);
                    var arraysubfamily = await ialmacenrepository.ListarSubFamiliaCodigo(tlsearch.Familia!, result2);
                    var arrayproveedor = await iproveedorrepository.ListadoNgSelectProveedorFamily(tlsearch.Familia!, result2);
                    Thread.Sleep(100);

                    SearchData searchData = new()
                    {
                        ListCodigoInterno = (List<TlProveedor>)arraylist,
                        ListFamilia = (List<TlFamilia>)arrayfamily,
                        ListSubFamilia = (List<TlSubFamilia>)arraysubfamily,
                        ListProveedor = (List<TlngselectProve>)arrayproveedor,
                    };
                    return Ok(searchData);
                }
                else if (tlsearch.Familia != "" && tlsearch.Subfamilia != "" && tlsearch.Proveedor == "")
                {
                    var arraylist = await iadicionalesrepository.ListarAnalisisFamiliaSubFamilia(
                        tlsearch.Limit,
                        tlsearch.Offset,
                        tlsearch.Familia!,
                        tlsearch.Subfamilia!,
                        result2);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaCodigo(result2);
                    var arraysubfamily = await ialmacenrepository.ListarSubFamiliaCodigo(tlsearch.Familia!, result2);
                    var arrayproveedor = await iproveedorrepository.ListadoNgSelectProveedorSubFamily(tlsearch.Familia!, tlsearch.Subfamilia!, result2);
                    Thread.Sleep(100);

                    SearchData searchData = new()
                    {
                        ListCodigoInterno = (List<TlProveedor>)arraylist,
                        ListFamilia = (List<TlFamilia>)arrayfamily,
                        ListSubFamilia = (List<TlSubFamilia>)arraysubfamily,
                        ListProveedor = (List<TlngselectProve>)arrayproveedor,
                    };
                    return Ok(searchData);
                }
                else if (tlsearch.Familia != "" && tlsearch.Subfamilia != "" && tlsearch.Proveedor != "")
                {
                    var arraylist = await iadicionalesrepository.ListarAnalisisFamiliaSubFamiliaProveedor(
                       tlsearch.Limit,
                       tlsearch.Offset,
                       tlsearch.Familia!,
                       tlsearch.Subfamilia!,
                       tlsearch.Proveedor!,
                       result2);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaCodigo(result2);
                    var arraysubfamily = await ialmacenrepository.ListarSubFamiliaCodigo(tlsearch.Familia!, result2);
                    var arrayproveedor = await iproveedorrepository.ListadoNgSelectProveedorSubFamily(tlsearch.Familia!, tlsearch.Subfamilia!, result2);
                    Thread.Sleep(100);

                    SearchData searchData = new()
                    {
                        ListCodigoInterno = (List<TlProveedor>)arraylist,
                        ListFamilia = (List<TlFamilia>)arrayfamily,
                        ListSubFamilia = (List<TlSubFamilia>)arraysubfamily,
                        ListProveedor = (List<TlngselectProve>)arrayproveedor,
                    };
                    return Ok(searchData);
                }
                /*----------------------------------------------------------------------------------*/
                /*----------------------------------------------------------------------------------*/
                /*----------------------------------------------------------------------------------*/
                /*----------------------------------------------------------------------------------*/
                /*----------------------------------------------------------------------------------*/
                else if (tlsearch.Familia == "" && tlsearch.Subfamilia != "" && tlsearch.Proveedor == "")
                {
                    var arraylist = await iadicionalesrepository.ListarAnalisis(
                       tlsearch.Limit,
                       tlsearch.Offset,
                       result2);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaCodigo(result2);
                    var arrayproveedor = await iproveedorrepository.ListadoNgselectProveedor(result2);
                    Thread.Sleep(100);

                    SearchData searchData = new()
                    {
                        ListCodigoInterno = (List<TlProveedor>)arraylist,
                        ListFamilia = (List<TlFamilia>)arrayfamily,
                        ListSubFamilia = [],
                        ListProveedor = (List<TlngselectProve>)arrayproveedor,
                    };
                    return Ok(searchData);
                }
                else if (tlsearch.Familia == "" && tlsearch.Subfamilia == "" && tlsearch.Proveedor != "")
                {
                    var arraylist = await iadicionalesrepository.ListarAnalisisProveedor(
                       tlsearch.Limit,
                       tlsearch.Offset,
                       tlsearch.Proveedor!,
                       result2);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaProveedor(tlsearch.Proveedor!, result2);
                    var arrayproveedor = await iproveedorrepository.ListadoNgselectProveedor(result2);
                    Thread.Sleep(100);

                    SearchData searchData = new()
                    {
                        ListCodigoInterno = (List<TlProveedor>)arraylist,
                        ListFamilia = (List<TlFamilia>)arrayfamily,
                        ListSubFamilia = [],
                        ListProveedor = (List<TlngselectProve>)arrayproveedor,
                    };
                    return Ok(searchData);
                }
                else if (tlsearch.Familia != "" && tlsearch.Subfamilia == "" && tlsearch.Proveedor != "")
                {
                    var arraylist = await iadicionalesrepository.ListarAnalisisFamiliaProveedor(
                     tlsearch.Limit,
                     tlsearch.Offset,
                     tlsearch.Familia!,
                     tlsearch.Proveedor!,
                     result2);
                    var arrayproveedor = await iproveedorrepository.ListadoNgSelectProveedorFamily(tlsearch.Familia!, result2);
                    var arraysubfamily = await ialmacenrepository.ListarSubFamiliaCodigo(tlsearch.Familia!, result2);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaProveedor(tlsearch.Proveedor!, result2);
                    Thread.Sleep(100);

                    SearchData searchData = new()
                    {
                        ListCodigoInterno = (List<TlProveedor>)arraylist,
                        ListFamilia = (List<TlFamilia>)arrayfamily,
                        ListSubFamilia = (List<TlSubFamilia>)arraysubfamily,
                        ListProveedor = (List<TlngselectProve>)arrayproveedor,
                    };
                    return Ok(searchData);
                }
                else if (tlsearch.Familia == "" && tlsearch.Subfamilia != "" && tlsearch.Proveedor != "")
                {
                    var arraylist = await iadicionalesrepository.ListarAnalisisProveedor(
                       tlsearch.Limit,
                       tlsearch.Offset,
                       tlsearch.Proveedor!,
                       result2);
                    var arrayproveedor = await iproveedorrepository.ListadoNgselectProveedor(result2);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaCodigo(result2);
                    Thread.Sleep(100);

                    SearchData searchData = new()
                    {
                        ListCodigoInterno = (List<TlProveedor>)arraylist,
                        ListFamilia = (List<TlFamilia>)arrayfamily,
                        ListSubFamilia = [],
                        ListProveedor = (List<TlngselectProve>)arrayproveedor,
                    };
                    return Ok(searchData);
                }
                else
                {
                    var arraylist = await iadicionalesrepository.ListarAnalisisProveedor(
                     tlsearch.Limit,
                     tlsearch.Offset,
                     tlsearch.Proveedor!,
                     result2);
                    var arrayproveedor = await iproveedorrepository.ListadoNgselectProveedor(result2);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaCodigo(result2);
                    Thread.Sleep(100);

                    SearchData searchData = new()
                    {
                        ListCodigoInterno = (List<TlProveedor>)arraylist,
                        ListFamilia = (List<TlFamilia>)arrayfamily,
                        ListSubFamilia = [],
                        ListProveedor = (List<TlngselectProve>)arrayproveedor,
                    };
                    return Ok(searchData);
                }
            }
            else
            {
                Thread.Sleep(100);
                return Ok("");
            }
        }

        [HttpPost("SearchArticulo")]
        public async Task<IActionResult> SearchArticulo([FromBody] TlbusquedaArticulo tlbusqueda)
        {
            var result1 = tlbusqueda.Fechaupload!.Replace("%2F", "/");
            var result2 = result1.Replace("%2F", "/");
            if (tlbusqueda.Tipoarticulo == "Codigo Interno")
            {
                var arrayCodigoInterno = await ialmacenrepository.ListarArticuloInventarioFilter(
                    tlbusqueda.Limit.ToString(),
                    tlbusqueda.Offset.ToString(),
                    tlbusqueda.CodigoArticulo!,
                    result2);
                Thread.Sleep(100);
                return Ok(arrayCodigoInterno);
            }
            else if (tlbusqueda.Tipoarticulo == "Codigo Equivalente")
            {
                var arrayCodigoInterno = await ialmacenrepository.FilterCodigoEqui(
                    tlbusqueda.Limit.ToString(),
                    tlbusqueda.Offset.ToString(),
                    tlbusqueda.CodigoArticulo!,
                    result2);
                Thread.Sleep(100);
                return Ok(arrayCodigoInterno);
            }
            else if (tlbusqueda.Tipoarticulo == "Descripcion")
            {
                var arrayDescripcion = await ialmacenrepository.FilterDescripcion(
                tlbusqueda.Limit.ToString(),
                tlbusqueda.Offset.ToString(),
                tlbusqueda.CodigoArticulo!,
                result2);
                Thread.Sleep(100);
                return Ok(arrayDescripcion);
            }
            else
            {
                Thread.Sleep(100);
                return Ok();
            }
        }

        [HttpPost("ActualizarReportes")]
        public async Task<IActionResult> ActualizarReportes([FromBody] TrReporte trReporte)
        {
            await ireporterepository.ActualizarAcumulados(trReporte);
            var reporte = await ireporterepository.ListarReporteFilter(trReporte.Codigo!);
            return Ok(reporte);
        }

        [HttpPost("DetailArticuloLote")]
        public async Task<IActionResult> DetailArticuloLote([FromBody] DetailSearch detsear)
        {
            var result1 = detsear.Fechaupload!.Replace("%2F", "/");
            var result2 = result1.Replace("%2F", "/");

            var Mes = DateTime.Now.ToString("MM");
            var Anio = DateTime.Now.ToString("yy");

            Meses(Mes, Anio);
            var result = await ialmacenrepository.DetalleInventarioGeneral(detsear.CodigoInterno!, result2);
            //Thread.Sleep(100);
            var listcountCant = await ialmacenrepository.ListadoCountCantidad(
                detsear.CodigoInterno!,
                montcant1,
                montcant2,
                montcant3,
                montcant4,
                montcant5,
                montcant6
                );
            //Thread.Sleep(100);
            var arrayloteDetalle = await ialmacenrepository.ListarDetalleLoteImportacionAll(detsear.CodigoInterno!, result2);
            //Thread.Sleep(100);
            var arraytotal = await ialmacenrepository.TotalDetalleLote(
                detsear.CodigoInterno!,
                detsear.Tc!,
                detsear.Desc!,
                detsear.Igv!, result2);
            //Thread.Sleep(100);
            var arrayPedido = await ialmacenrepository.TotalPedidoAnalisis(detsear.CodigoInterno!);
            //Thread.Sleep(100);
            var arrayReporte = await ireporterepository.ListarReporteFilter(detsear.CodigoInterno!);
            //Thread.Sleep(100);
            var arraydetalleimp = await ialmacenrepository.DetalleImportacion(detsear.CodigoInterno!);
            Thread.Sleep(100);

            DetailArticulo detarti = new()
            {
                /*---------------------------------------------*/
                /*---------------------------------------------*/
                Fechaacumulado = arraydetalleimp == null ? "" : arraydetalleimp.Fechaacumulado,
                Nombrepedido = arraydetalleimp == null ? "" : arraydetalleimp.Nombrepedido,
                Cantidadpedido = arraydetalleimp == null ? "" : arraydetalleimp.Cantidadpedido,
                Nombreviajando = arraydetalleimp == null ? "" : arraydetalleimp.Nombreviajando,
                Cantidadviajando = arraydetalleimp == null ? "" : arraydetalleimp.Cantidadviajando,
                Sumaviajando = arraydetalleimp == null ? "" : arraydetalleimp.Sumaviajando,
                /*---------------------------------------------*/
                /*---------------------------------------------*/
                Ajustesfecha = arrayReporte == null ? "" : arrayReporte.Ajustesfecha,
                Promes2024 = arrayReporte == null ? "" : arrayReporte.Promes2024,
                Promes2023 = arrayReporte == null ? "" : arrayReporte.Promes2023,
                Stockpedido = arrayReporte == null ? "" : arrayReporte.Stockpedido,
                Stockviaje = arrayReporte == null ? "" : arrayReporte.Stockviaje,
                Stockalma = arrayReporte == null ? "" : arrayReporte.Stockalma,
                Mesesrese = arrayReporte == null ? "" : arrayReporte.Mesesrese,
                Pedidorecom = arrayReporte == null ? "" : arrayReporte.Pedidorecom,
                Ordenpedido = arrayReporte == null ? "" : arrayReporte.Ordenpedido,
                /*---------------------------------------------*/
                /*---------------------------------------------*/
                Pedido = arrayPedido == null ? "" : arrayPedido.Pedido,
                /*---------------------------------------------*/
                Month1 = listcountCant.Month1.ToString(),
                Month2 = listcountCant.Month2.ToString(),
                Month3 = listcountCant.Month3.ToString(),
                Month4 = listcountCant.Month4.ToString(),
                Month5 = listcountCant.Month5.ToString(),
                Month6 = listcountCant.Month6.ToString(),
                /*---------------------------------------------*/
                Totallote = arraytotal.Totallote,
                Totalcostolote = arraytotal.Totalcostolote,
                Totalcostounitario = arraytotal.Totalcostounitario,
                Totacampo = arraytotal.Totacampo,
                Totalutilprom = arraytotal.Totalutilprom,
                /*---------------------------------------------*/
                Codart = result.Codart,
                Codequi = result.Codequi,
                Descripcion = result.Descripcion,
                Cantidad_solicitada = result.Cantidad_solicitada,
                Stock_viaje = result.Stock_viaje,
                Stock_proy = result.Stock_proy,
                Total_stock = result.Total_stock,
                Otros_motoline = result.Otros_motoline,
                Stock_ate = result.Stock_ate,
                Stock_abtao = result.Stock_abtao,
                Stock_brillantes = result.Stock_brillantes,
                Stock_iquitos = result.Stock_iquitos,
                Salidas_consumo = result.Salidas_consumo,
                Prom_venta_2024 = result.Prom_venta_2024,
                Prom_venta_2023 = result.Prom_venta_2023,
                Total_ventas = result.Total_ventas,
                Valor_fob_ultimo = result.Valor_fob_ultimo,
                Valor_fob_2019_antes = result.Valor_fob_2019_antes,
                Precio_vta_actual = result.Precio_vta_actual,
                Precio_venta_2019 = result.Precio_venta_2019,
                Comprometido = result.Comprometido,
                Ventas_2024_ate = result.Ventas_2024_ate,
                Ventas_2024_abtao = result.Ventas_2024_abtao,
                Ventas_2024_brillantes = result.Ventas_2024_brillantes,
                Ventas_2024_iquitos = result.Ventas_2024_iquitos,
                Ventas_2023 = result.Ventas_2023,
                Ventas_2022 = result.Ventas_2022,
                Ventas_2021 = result.Ventas_2021,
                Ventas_2020 = result.Ventas_2020,
                Ventas_2019 = result.Ventas_2019,
                Ventas_2013 = result.Ventas_2013,
                Fecha_ult_venta = result.Fecha_ult_venta,
                Fecha_ult_compra = result.Fecha_ult_compra,
                Compra_2024_total = result.Compra_2024_total,
                Compra_2024_ultima = result.Compra_2024_ultima,
                Compra_2023_total = result.Compra_2023_total,
                Compra_2023_ultima = result.Compra_2023_ultima,
                Total_compra = result.Total_compra,
                Stock_en_transito = result.Stock_en_transito,
                Transf_gratuita_del_mes = result.Transf_gratuita_del_mes,
                Correccion_de_cod_tip_mov = result.Correccion_de_cod_tip_mov,
                Correccion_de_cod_cantidad = result.Correccion_de_cod_cantidad,
                Valor_vta_soles = result.Valor_vta_soles,
                Valor_vta_dolar = result.Valor_vta_dolar,
                Ultimo_costo_prom_soles = result.Ultimo_costo_prom_soles,
                Ultimo_costo_prom_dolar = result.Ultimo_costo_prom_dolar,
                Margen_unit_soles = result.Margen_unit_soles,
                Margen_unit_dolar = result.Margen_unit_dolar,
                Margen_unitario = result.Margen_unitario,
                Utilidad_por_ventas = result.Utilidad_por_ventas,
                Abs_x_participacion = result.Abs_x_participacion,
                Indice_rotacion = result.Indice_rotacion,
                Prom_ventas_ult_mes = result.Prom_ventas_ult_mes,
                Nro_meses_stock = result.Nro_meses_stock,
                Proveedor_nombre = result.Proveedor_nombre,
                Proveedor_codigo = result.Proveedor_codigo,
                Familia_codigo = result.Familia_codigo,
                Familia_descripcion = result.Familia_descripcion,
                Sub_familia_codigo = result.Sub_familia_codigo,
                Sub_familia_descripcion = result.Sub_familia_descripcion,
                /*---------------------------------------------*/
                ListDetalleLote = (List<TlDetallelote>)arrayloteDetalle,
            };
            return Ok(detarti);
        }
        public static void Meses(string Mes, string Anio)
        {
            if (Int32.Parse(Mes) == 1)
            {
                mont1 = "Enero " + Anio;
                mont2 = "Diciembre " + (int.Parse(Anio) - 1);
                mont3 = "Noviembre " + (int.Parse(Anio) - 1);
                mont4 = "Octubre " + (int.Parse(Anio) - 1);
                mont5 = "Septiembre " + (int.Parse(Anio) - 1);
                mont6 = "Agosto " + (int.Parse(Anio) - 1);
                /*--------------------------------------*/
                montcant1 = "01-" + Anio;
                montcant2 = "12-" + (int.Parse(Anio) - 1);
                montcant3 = "11-" + (int.Parse(Anio) - 1);
                montcant4 = "10-" + (int.Parse(Anio) - 1);
                montcant5 = "09-" + (int.Parse(Anio) - 1);
                montcant6 = "08-" + (int.Parse(Anio) - 1);
            }
            else if (Int32.Parse(Mes) == 2)
            {
                mont1 = "Febrero " + Anio;
                mont2 = "Enero " + Anio;
                mont3 = "Diciembre " + (int.Parse(Anio) - 1);
                mont4 = "Noviembre " + (int.Parse(Anio) - 1);
                mont5 = "Octubre " + (int.Parse(Anio) - 1);
                mont6 = "Septiembre " + (int.Parse(Anio) - 1);
                /*--------------------------------------*/
                montcant1 = "02-" + Anio;
                montcant2 = "01-" + Anio;
                montcant3 = "12-" + (int.Parse(Anio) - 1);
                montcant4 = "11-" + (int.Parse(Anio) - 1);
                montcant5 = "10-" + (int.Parse(Anio) - 1);
                montcant6 = "09-" + (int.Parse(Anio) - 1);
            }
            else if (Int32.Parse(Mes) == 3)
            {
                mont1 = "Marzo " + Anio;
                mont2 = "Febrero " + Anio;
                mont3 = "Enero " + Anio;
                mont4 = "Diciembre " + (int.Parse(Anio) - 1);
                mont5 = "Noviembre " + (int.Parse(Anio) - 1);
                mont6 = "Octubre " + (int.Parse(Anio) - 1);
                /*--------------------------------------*/
                montcant1 = "03-" + Anio;
                montcant2 = "02-" + Anio;
                montcant3 = "01-" + Anio;
                montcant4 = "12-" + (int.Parse(Anio) - 1);
                montcant5 = "11-" + (int.Parse(Anio) - 1);
                montcant6 = "10-" + (int.Parse(Anio) - 1);
            }
            else if (Int32.Parse(Mes) == 4)
            {
                mont1 = "Abril " + Anio;
                mont2 = "Marzo " + Anio;
                mont3 = "Febrero " + Anio;
                mont4 = "Enero " + Anio;
                mont5 = "Diciembre " + (int.Parse(Anio) - 1);
                mont6 = "Noviembre " + (int.Parse(Anio) - 1);
                /*--------------------------------------*/
                montcant1 = "04-" + Anio;
                montcant2 = "03-" + Anio;
                montcant3 = "02-" + Anio;
                montcant4 = "01-" + Anio;
                montcant5 = "12-" + (int.Parse(Anio) - 1);
                montcant6 = "11-" + (int.Parse(Anio) - 1);
            }
            else if (Int32.Parse(Mes) == 5)
            {
                mont1 = "Mayo " + Anio;
                mont2 = "Abril " + Anio;
                mont3 = "Marzo " + Anio;
                mont4 = "Febrero " + Anio;
                mont5 = "Enero " + Anio;
                mont6 = "Diciembre " + (int.Parse(Anio) - 1);
                /*--------------------------------------*/
                montcant1 = "05-" + Anio;
                montcant2 = "04-" + Anio;
                montcant3 = "03-" + Anio;
                montcant4 = "02-" + Anio;
                montcant5 = "01-" + Anio;
                montcant6 = "12-" + (int.Parse(Anio) - 1);
            }
            else if (Int32.Parse(Mes) == 6)
            {
                mont1 = "Junio " + Anio;
                mont2 = "Mayo " + Anio;
                mont3 = "Abril " + Anio;
                mont4 = "Marzo " + Anio;
                mont5 = "Febrero " + Anio;
                mont6 = "Enero " + Anio;
                /*--------------------------------------*/
                montcant1 = "06-" + Anio;
                montcant2 = "05-" + Anio;
                montcant3 = "04-" + Anio;
                montcant4 = "03-" + Anio;
                montcant5 = "02-" + Anio;
                montcant6 = "01-" + Anio;
            }
            else if (Int32.Parse(Mes) == 7)
            {
                mont1 = "Julio " + Anio;
                mont2 = "Junio " + Anio;
                mont3 = "Mayo " + Anio;
                mont4 = "Abril " + Anio;
                mont5 = "Marzo " + Anio;
                mont6 = "Febrero " + Anio;
                /*--------------------------------------*/
                montcant1 = "07-" + Anio;
                montcant2 = "06-" + Anio;
                montcant3 = "05-" + Anio;
                montcant4 = "04-" + Anio;
                montcant5 = "03-" + Anio;
                montcant6 = "02-" + Anio;
            }
            else if (Int32.Parse(Mes) == 8)
            {
                mont1 = "Agosto " + Anio;
                mont2 = "Julio " + Anio;
                mont3 = "Junio " + Anio;
                mont4 = "Mayo " + Anio;
                mont5 = "Abril " + Anio;
                mont6 = "Marzo " + Anio;
                /*--------------------------------------*/
                montcant1 = "08-" + Anio;
                montcant2 = "07-" + Anio;
                montcant3 = "06-" + Anio;
                montcant4 = "05-" + Anio;
                montcant5 = "04-" + Anio;
                montcant6 = "03-" + Anio;
            }
            else if (Int32.Parse(Mes) == 9)
            {
                mont1 = "Septiembre " + Anio;
                mont2 = "Agosto " + Anio;
                mont3 = "Julio " + Anio;
                mont4 = "Junio " + Anio;
                mont5 = "Mayo " + Anio;
                mont6 = "Abril " + Anio;
                /*--------------------------------------*/
                montcant1 = "09-" + Anio;
                montcant2 = "08-" + Anio;
                montcant3 = "07-" + Anio;
                montcant4 = "06-" + Anio;
                montcant5 = "05-" + Anio;
                montcant6 = "04-" + Anio;
            }
            else if (Int32.Parse(Mes) == 10)
            {
                mont1 = "Octubre " + Anio;
                mont2 = "Septiembre " + Anio;
                mont3 = "Agosto " + Anio;
                mont4 = "Julio " + Anio;
                mont5 = "Junio " + Anio;
                mont6 = "Mayo " + Anio;
                /*--------------------------------------*/
                montcant1 = "10-" + Anio;
                montcant2 = "09-" + Anio;
                montcant3 = "08-" + Anio;
                montcant4 = "07-" + Anio;
                montcant5 = "06-" + Anio;
                montcant6 = "05-" + Anio;
            }
            else if (Int32.Parse(Mes) == 11)
            {
                mont1 = "Noviembre " + Anio;
                mont2 = "Octubre " + Anio;
                mont3 = "Septiembre " + Anio;
                mont4 = "Agosto " + Anio;
                mont5 = "Julio " + Anio;
                mont6 = "Junio " + Anio;
                /*--------------------------------------*/
                montcant1 = "11-" + Anio;
                montcant2 = "10-" + Anio;
                montcant3 = "09-" + Anio;
                montcant4 = "08-" + Anio;
                montcant5 = "07-" + Anio;
                montcant6 = "06-" + Anio;
            }
            else if (Int32.Parse(Mes) == 12)
            {
                mont1 = "Diciembre " + Anio;
                mont2 = "Noviembre " + Anio;
                mont3 = "Octubre " + Anio;
                mont4 = "Septiembre " + Anio;
                mont5 = "Agosto " + Anio;
                mont6 = "Julio " + Anio;
                /*--------------------------------------*/
                montcant1 = "12-" + Anio;
                montcant2 = "11-" + Anio;
                montcant3 = "10-" + Anio;
                montcant4 = "09-" + Anio;
                montcant5 = "08-" + Anio;
                montcant6 = "07-" + Anio;
            }
        }
    }

    public class DetailSearch
    {
        public string? CodigoInterno { get; set; }
        public string? Tc { get; set; }
        public string? Desc { get; set; }
        public float Igv { get; set; }
        public string? Fechaupload { get; set; }
    }
    public class DetailArticulo
    {
        /*---------------------------------------------*/
        /*---------------------------------------------*/
        public string? Fechaacumulado { get; set; }
        public string? Nombrepedido { get; set; }
        public string? Cantidadpedido { get; set; }
        public string? Nombreviajando { get; set; }
        public string? Cantidadviajando { get; set; }
        public string? Sumaviajando { get; set; }
        /*---------------------------------------------*/
        /*---------------------------------------------*/
        public string? Ajustesfecha { get; set; }
        public string? Promes2024 { get; set; }
        public string? Promes2023 { get; set; }
        public string? Stockpedido { get; set; }
        public string? Stockviaje { get; set; }
        public string? Stockalma { get; set; }
        public string? Mesesrese { get; set; }
        public string? Pedidorecom { get; set; }
        public string? Ordenpedido { get; set; }
        /*---------------------------------------------*/
        /*---------------------------------------------*/
        public string? Pedido { get; set; }
        /*---------------------------------------------*/
        public int Totallote { get; set; }
        public float Totalcostolote { get; set; }
        public float Totalcostounitario { get; set; }
        public float Totacampo { get; set; }
        public float Totalutilprom { get; set; }
        /*---------------------------------------------*/
        public string? Month1 { get; set; }
        public string? Month2 { get; set; }
        public string? Month3 { get; set; }
        public string? Month4 { get; set; }
        public string? Month5 { get; set; }
        public string? Month6 { get; set; }
        /*---------------------------------------------*/
        public string? Codart { get; set; }
        public string? Codequi { get; set; }
        public string? Descripcion { get; set; }
        public string? Cantidad_solicitada { get; set; }
        public string? Stock_viaje { get; set; }
        public string? Stock_proy { get; set; }
        public string? Total_stock { get; set; }
        public string? Otros_motoline { get; set; }
        public string? Stock_ate { get; set; }
        public string? Stock_abtao { get; set; }
        public string? Stock_brillantes { get; set; }
        public string? Stock_iquitos { get; set; }
        public string? Salidas_consumo { get; set; }
        public string? Prom_venta_2024 { get; set; }
        public string? Prom_venta_2023 { get; set; }
        public string? Total_ventas { get; set; }
        public string? Valor_fob_ultimo { get; set; }
        public string? Valor_fob_2019_antes { get; set; }
        public string? Precio_vta_actual { get; set; }
        public string? Precio_venta_2019 { get; set; }
        public string? Comprometido { get; set; }
        public string? Ventas_2024_ate { get; set; }
        public string? Ventas_2024_abtao { get; set; }
        public string? Ventas_2024_brillantes { get; set; }
        public string? Ventas_2024_iquitos { get; set; }
        public string? Ventas_2023 { get; set; }
        public string? Ventas_2022 { get; set; }
        public string? Ventas_2021 { get; set; }
        public string? Ventas_2020 { get; set; }
        public string? Ventas_2019 { get; set; }
        public string? Ventas_2013 { get; set; }
        public string? Fecha_ult_venta { get; set; }
        public string? Fecha_ult_compra { get; set; }
        public string? Compra_2024_total { get; set; }
        public string? Compra_2024_ultima { get; set; }
        public string? Compra_2023_total { get; set; }
        public string? Compra_2023_ultima { get; set; }
        public string? Total_compra { get; set; }
        public string? Stock_en_transito { get; set; }
        public string? Transf_gratuita_del_mes { get; set; }
        public string? Correccion_de_cod_tip_mov { get; set; }
        public string? Correccion_de_cod_cantidad { get; set; }
        public string? Valor_vta_soles { get; set; }
        public string? Valor_vta_dolar { get; set; }
        public string? Ultimo_costo_prom_soles { get; set; }
        public string? Ultimo_costo_prom_dolar { get; set; }
        public string? Margen_unit_soles { get; set; }
        public string? Margen_unit_dolar { get; set; }
        public string? Margen_unitario { get; set; }
        public string? Utilidad_por_ventas { get; set; }
        public string? Abs_x_participacion { get; set; }
        public string? Indice_rotacion { get; set; }
        public string? Prom_ventas_ult_mes { get; set; }
        public string? Nro_meses_stock { get; set; }
        public string? Proveedor_nombre { get; set; }
        public string? Proveedor_codigo { get; set; }
        public string? Familia_codigo { get; set; }
        public string? Familia_descripcion { get; set; }
        public string? Sub_familia_codigo { get; set; }
        public string? Sub_familia_descripcion { get; set; }
        /*---------------------------------------------*/
        public List<TlDetallelote>? ListDetalleLote { get; set; }
    }
    public class TlbusquedaArticulo
    {
        public string? Tipoarticulo { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
        public string? CodigoArticulo { get; set; }
        public string? Fechaupload { get; set; }
    }
    public class TlSearchCodigo
    {
        public string? Familia { get; set; }
        public string? Subfamilia { get; set; }
        public string? Tipoarticulo { get; set; }
        public string? Proveedor { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
        public string? Fechaupload { get; set; }
    }
    public class TlAllData
    {
        public string? Month1 { get; set; }
        public string? Month2 { get; set; }
        public string? Month3 { get; set; }
        public string? Month4 { get; set; }
        public string? Month5 { get; set; }
        public string? Month6 { get; set; }
        public List<TipoAr>? ListTipoArti { get; set; }
        public List<TipoPro>? ListTipoProve { get; set; }
        public List<Meses>? ListMeses { get; set; }
        public List<TlFamilia>? ListFamily { get; set; }
        public List<TlngselectProve>? Listproveedor { get; set; }
        public List<TlProveedor>? ListCodigoInterno { get; set; }
    }
    public class SearchData
    {
        public List<TlProveedor>? ListCodigoInterno { get; set; }
        public List<TlFamilia>? ListFamilia { get; set; }
        public List<TlSubFamilia>? ListSubFamilia { get; set; }
        public List<TlngselectProve>? ListProveedor { get; set; }
    }
    public class TipoPro
    {
        public string? TipoProve { get; set; }
    }
    public class TipoAr
    {
        public string? TipoArticulo { get; set; }
    }
    public class Meses
    {
        public string? Month { get; set; }
    }
}

