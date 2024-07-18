using ApiAlmacen.Repository.AcumuladoRepository.Interface;
using ApiAlmacen.Repository.AdicionalesRepository.Interface;
using ApiAlmacen.Repository.AlmacenRepository.Interface;
using ApiAlmacen.Repository.AlmacenRepository.Models;
using ApiAlmacen.Repository.ProveedorRepository.Interface;
using ApiAlmacen.Repository.ProveedorRepository.Models;
using ApiAlmacen.Repository.ReporteRepository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ApiAlmacen.Controllers.Adicionales
{
    [Route("api/[controller]")]
    [ApiController]
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

        [HttpGet("ListarAllIniciales")]
        public async Task<IActionResult> ListarAllIniciales()
        {
            var Mes = DateTime.Now.ToString("MM");
            var Anio = DateTime.Now.ToString("yyyy");

            Meses(Mes, Anio);
            //var result = mesespanol.ToString(cultures);

            // var result = mont1 + " " + mont2 + " " + mont3 + " " + mont4 + " " + mont5 + " " + mont6;

            int Limit = 250;
            int Offset = 0;
            var arrayfamily = await ialmacenrepository.ListarFamiliaCodigo();
            var arrayproveedor = await iproveedorrepository.ListadoNgselectProveedor();
            var arraycodigointerno = await iproveedorrepository.ListadoProveedorAll(Limit, Offset);
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
            if (tlsearch.Tipoarticulo == "Codigo Interno")
            {
                if (tlsearch.Familia == "" && tlsearch.Subfamilia == "" && tlsearch.Proveedor == "")
                {
                    var arraylist = await iadicionalesrepository.ListarAnalisis(
                        tlsearch.Limit,
                        tlsearch.Offset);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaCodigo();
                    var arrayproveedor = await iproveedorrepository.ListadoNgselectProveedor();
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
                        tlsearch.Familia!);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaCodigo();
                    var arraysubfamily = await ialmacenrepository.ListarSubFamiliaCodigo(tlsearch.Familia!);
                    var arrayproveedor = await iproveedorrepository.ListadoNgSelectProveedorFamily(tlsearch.Familia!);
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
                        tlsearch.Subfamilia!);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaCodigo();
                    var arraysubfamily = await ialmacenrepository.ListarSubFamiliaCodigo(tlsearch.Familia!);
                    var arrayproveedor = await iproveedorrepository.ListadoNgSelectProveedorSubFamily(tlsearch.Familia!, tlsearch.Subfamilia!);
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
                       tlsearch.Proveedor!);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaCodigo();
                    var arraysubfamily = await ialmacenrepository.ListarSubFamiliaCodigo(tlsearch.Familia!);
                    var arrayproveedor = await iproveedorrepository.ListadoNgSelectProveedorSubFamily(tlsearch.Familia!, tlsearch.Subfamilia!);
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
                       tlsearch.Offset);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaCodigo();
                    var arrayproveedor = await iproveedorrepository.ListadoNgselectProveedor();
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
                       tlsearch.Proveedor!);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaProveedor(tlsearch.Proveedor!);
                    var arrayproveedor = await iproveedorrepository.ListadoNgselectProveedor();
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
                     tlsearch.Proveedor!);
                    var arrayproveedor = await iproveedorrepository.ListadoNgSelectProveedorFamily(tlsearch.Familia!);
                    var arraysubfamily = await ialmacenrepository.ListarSubFamiliaCodigo(tlsearch.Familia!);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaProveedor(tlsearch.Proveedor!);
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
                       tlsearch.Proveedor!);
                    var arrayproveedor = await iproveedorrepository.ListadoNgselectProveedor();
                    var arrayfamily = await ialmacenrepository.ListarFamiliaCodigo();
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
                     tlsearch.Proveedor!);
                    var arrayproveedor = await iproveedorrepository.ListadoNgselectProveedor();
                    var arrayfamily = await ialmacenrepository.ListarFamiliaCodigo();
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
                        tlsearch.Offset);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaCodigo();
                    var arrayproveedor = await iproveedorrepository.ListadoNgselectProveedor();
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
                        tlsearch.Familia!);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaCodigo();
                    var arraysubfamily = await ialmacenrepository.ListarSubFamiliaCodigo(tlsearch.Familia!);
                    var arrayproveedor = await iproveedorrepository.ListadoNgSelectProveedorFamily(tlsearch.Familia!);
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
                        tlsearch.Subfamilia!);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaCodigo();
                    var arraysubfamily = await ialmacenrepository.ListarSubFamiliaCodigo(tlsearch.Familia!);
                    var arrayproveedor = await iproveedorrepository.ListadoNgSelectProveedorSubFamily(tlsearch.Familia!, tlsearch.Subfamilia!);
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
                       tlsearch.Proveedor!);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaCodigo();
                    var arraysubfamily = await ialmacenrepository.ListarSubFamiliaCodigo(tlsearch.Familia!);
                    var arrayproveedor = await iproveedorrepository.ListadoNgSelectProveedorSubFamily(tlsearch.Familia!, tlsearch.Subfamilia!);
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
                       tlsearch.Offset);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaCodigo();
                    var arrayproveedor = await iproveedorrepository.ListadoNgselectProveedor();
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
                       tlsearch.Proveedor!);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaProveedor(tlsearch.Proveedor!);
                    var arrayproveedor = await iproveedorrepository.ListadoNgselectProveedor();
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
                     tlsearch.Proveedor!);
                    var arrayproveedor = await iproveedorrepository.ListadoNgSelectProveedorFamily(tlsearch.Familia!);
                    var arraysubfamily = await ialmacenrepository.ListarSubFamiliaCodigo(tlsearch.Familia!);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaProveedor(tlsearch.Proveedor!);
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
                       tlsearch.Proveedor!);
                    var arrayproveedor = await iproveedorrepository.ListadoNgselectProveedor();
                    var arrayfamily = await ialmacenrepository.ListarFamiliaCodigo();
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
                     tlsearch.Proveedor!);
                    var arrayproveedor = await iproveedorrepository.ListadoNgselectProveedor();
                    var arrayfamily = await ialmacenrepository.ListarFamiliaCodigo();
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
                        tlsearch.Offset);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaCodigo();
                    var arrayproveedor = await iproveedorrepository.ListadoNgselectProveedor();
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
                        tlsearch.Familia!);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaCodigo();
                    var arraysubfamily = await ialmacenrepository.ListarSubFamiliaCodigo(tlsearch.Familia!);
                    var arrayproveedor = await iproveedorrepository.ListadoNgSelectProveedorFamily(tlsearch.Familia!);
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
                        tlsearch.Subfamilia!);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaCodigo();
                    var arraysubfamily = await ialmacenrepository.ListarSubFamiliaCodigo(tlsearch.Familia!);
                    var arrayproveedor = await iproveedorrepository.ListadoNgSelectProveedorSubFamily(tlsearch.Familia!, tlsearch.Subfamilia!);
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
                       tlsearch.Proveedor!);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaCodigo();
                    var arraysubfamily = await ialmacenrepository.ListarSubFamiliaCodigo(tlsearch.Familia!);
                    var arrayproveedor = await iproveedorrepository.ListadoNgSelectProveedorSubFamily(tlsearch.Familia!, tlsearch.Subfamilia!);
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
                       tlsearch.Offset);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaCodigo();
                    var arrayproveedor = await iproveedorrepository.ListadoNgselectProveedor();
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
                       tlsearch.Proveedor!);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaProveedor(tlsearch.Proveedor!);
                    var arrayproveedor = await iproveedorrepository.ListadoNgselectProveedor();
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
                     tlsearch.Proveedor!);
                    var arrayproveedor = await iproveedorrepository.ListadoNgSelectProveedorFamily(tlsearch.Familia!);
                    var arraysubfamily = await ialmacenrepository.ListarSubFamiliaCodigo(tlsearch.Familia!);
                    var arrayfamily = await ialmacenrepository.ListarFamiliaProveedor(tlsearch.Proveedor!);
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
                       tlsearch.Proveedor!);
                    var arrayproveedor = await iproveedorrepository.ListadoNgselectProveedor();
                    var arrayfamily = await ialmacenrepository.ListarFamiliaCodigo();
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
                     tlsearch.Proveedor!);
                    var arrayproveedor = await iproveedorrepository.ListadoNgselectProveedor();
                    var arrayfamily = await ialmacenrepository.ListarFamiliaCodigo();
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
            }
            else if (Int32.Parse(Mes) == 2)
            {
                mont1 = "Febrero " + Anio;
                mont2 = "Enero " + Anio;
                mont3 = "Diciembre " + (int.Parse(Anio) - 1);
                mont4 = "Noviembre " + (int.Parse(Anio) - 1);
                mont5 = "Octubre " + (int.Parse(Anio) - 1);
                mont6 = "Septiembre " + (int.Parse(Anio) - 1);
            }
            else if (Int32.Parse(Mes) == 3)
            {
                mont1 = "Marzo " + Anio;
                mont2 = "Febrero " + Anio;
                mont3 = "Enero " + Anio;
                mont4 = "Diciembre " + (int.Parse(Anio) - 1);
                mont5 = "Noviembre " + (int.Parse(Anio) - 1);
                mont6 = "Octubre " + (int.Parse(Anio) - 1);
            }
            else if (Int32.Parse(Mes) == 4)
            {
                mont1 = "Abril " + Anio;
                mont2 = "Marzo " + Anio;
                mont3 = "Febrero " + Anio;
                mont4 = "Enero " + Anio;
                mont5 = "Diciembre " + (int.Parse(Anio) - 1);
                mont6 = "Noviembre " + (int.Parse(Anio) - 1);
            }
            else if (Int32.Parse(Mes) == 5)
            {
                mont1 = "Mayo " + Anio;
                mont2 = "Abril " + Anio;
                mont3 = "Marzo " + Anio;
                mont4 = "Febrero " + Anio;
                mont5 = "Enero " + Anio;
                mont6 = "Diciembre " + (int.Parse(Anio) - 1);
            }
            else if (Int32.Parse(Mes) == 6)
            {
                mont1 = "Junio " + Anio;
                mont2 = "Mayo " + Anio;
                mont3 = "Abril " + Anio;
                mont4 = "Marzo " + Anio;
                mont5 = "Febrero " + Anio;
                mont6 = "Enero " + Anio;
            }

            else if (Int32.Parse(Mes) == 7)
            {
                mont1 = "Julio " + Anio;
                mont2 = "Junio " + Anio;
                mont3 = "Mayo " + Anio;
                mont4 = "Abril " + Anio;
                mont5 = "Marzo " + Anio;
                mont6 = "Febrero " + Anio;
            }
            else if (Int32.Parse(Mes) == 8)
            {
                mont1 = "Agosto " + Anio;
                mont2 = "Julio " + Anio;
                mont3 = "Junio " + Anio;
                mont4 = "Mayo " + Anio;
                mont5 = "Abril " + Anio;
                mont6 = "Marzo " + Anio;
            }

            else if (Int32.Parse(Mes) == 9)
            {
                mont1 = "Septiembre " + Anio;
                mont2 = "Agosto " + Anio;
                mont3 = "Julio " + Anio;
                mont4 = "Junio " + Anio;
                mont5 = "Mayo " + Anio;
                mont6 = "Abril " + Anio;
            }

            else if (Int32.Parse(Mes) == 10)
            {
                mont1 = "Octubre " + Anio;
                mont2 = "Septiembre " + Anio;
                mont3 = "Agosto " + Anio;
                mont4 = "Julio " + Anio;
                mont5 = "Junio " + Anio;
                mont6 = "Mayo " + Anio;
            }

            else if (Int32.Parse(Mes) == 11)
            {
                mont1 = "Noviembre " + Anio;
                mont2 = "Octubre " + Anio;
                mont3 = "Septiembre " + Anio;
                mont4 = "Agosto " + Anio;
                mont5 = "Julio " + Anio;
                mont6 = "Junio " + Anio;
            }

            else if (Int32.Parse(Mes) == 12)
            {
                mont1 = "Diciembre " + Anio;
                mont2 = "Noviembre " + Anio;
                mont3 = "Octubre " + Anio;
                mont4 = "Septiembre " + Anio;
                mont5 = "Agosto " + Anio;
                mont6 = "Julio " + Anio;
            }
        }

    }
    public class TlSearchCodigo
    {
        public string? Familia { get; set; }
        public string? Subfamilia { get; set; }
        public string? Tipoarticulo { get; set; }
        public string? Proveedor { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
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

