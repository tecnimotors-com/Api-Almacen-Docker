

using ApiAlmacen.Repository.AlmacenRepository.Models;

namespace ApiAlmacen.Repository.AlmacenRepository.Interface
{
    public interface IAlmacenRepository
    {
        public Task<IEnumerable<TrAlmacen>> ListarProductoAlmacen(string Articulo);
        public Task<IEnumerable<TlAlmacenBuscar>> ListarProductoCompletoAlmacen(string Articulo);
        public Task<IEnumerable<TlAlmacenBuscar>> ListarProductoCompletoAlmacen2();
        public Task<bool> RegistrarAlmacen(TrAlmacen trAlmacen);
        /*------------------------------------------------------------------------------*/
        /*------------------------------------------------------------------------------*/
        /*------------------------------------------------------------------------------*/
        /*------------------------------------------------------------------------------*/
        public Task<IEnumerable<TlInventario>> ListarAnalisisCostosInventario(string Limit, string Offset, string Fecha_upload);
        /*------------------------------------------------------------------------------*/
        /*------------------------------------------------------------------------------*/
        public Task<IEnumerable<TlFamilia>> ListarFamiliaCodigo(string Fecha_upload); 
        public Task<IEnumerable<TlFamilia>> ListarFamiliaProveedor(string ProveedorCodigo, string Fecha_upload);
        public Task<IEnumerable<TlSubFamilia>> ListarSubFamiliaCodigo(string Familia, string Fecha_upload);
        public Task<IEnumerable<TlInventario>> ListarInventarioGeneral(string Limit, string Offset, string Familia, string SubFamilia, string Fecha_upload);
        /*------------------------------------------------------------------------------*/
        /*------------------------------------------------------------------------------*/
        public Task<IEnumerable<TlArticulo>> ListarArticuloInventarioOnly(string Fecha_upload);
        public Task<IEnumerable<TlArticulo>> ListarEquivalentoInventarioOnly(string Fecha_upload);
        public Task<IEnumerable<TlArticulo>> ListarDescripcionInventariOnly(string Fecha_upload);
        /*------------------------------------------------------------------------------*/
        public Task<IEnumerable<TlArticuloAcumulado>> ListarArticuloInventario(string Limit, string Offset, string Fecha_upload);
        public Task<IEnumerable<TlCodiEqui>> ListarEquivalentoInventario(string Limit, string Offset, string Fecha_upload);
        public Task<IEnumerable<TlDescrip>> ListarDescripcionInventario(string Limit, string Offset, string Fecha_upload);
        /*------------------------------------------------------------------------------*/
        /*------------------------------------------------------------------------------*/
        /*------------------------------------------------------------------------------*/
        public Task<IEnumerable<TlArticuloAcumulado>> ListarArticuloInventarioFilter(string Limit, string Offset, string Articulo, string Fecha_upload);
        public Task<TlInventario> DetalleInventarioGeneral(string Articulo, string Fecha_upload);
        public Task<TlMonthList> ListadoCountCantidad(string Articulo, string Mes1, string Mes2, string Mes3, string Mes4, string Mes5, string Mes6);
        public Task<IEnumerable<TlDetallelote>> ListarDetalleLoteImportacion(string Limit, string Offset, string Articulo, string Fecha_upload);
        public Task<IEnumerable<TlDetallelote>> ListarDetalleLoteImportacionAll(string Articulo, string Fecha_upload);
        public Task<TlDetLote> TotalDetalleLote(string Articulo, string Tc, string Desc, float Igv, string Fecha_upload);
        public Task<TlPedidos> TotalPedidoAnalisis(string Articulo);
        /*------------------------------------------------------------------*/
        /*------------------------------------------------------------------*/
        /*------------------------------------------------------------------*/
        public Task<IEnumerable<TlArticulo>> FilterCodigoInterno(string Limit, string Offset, string CodiInte, string Fecha_upload);
        /*------------------------------------------------------------------*/
        public Task<IEnumerable<TlCodiEqui>> FilterCodigoEqui(string Limit, string Offset, string CodigoEqui, string Fecha_upload);
        /*------------------------------------------------------------------*/
        public Task<IEnumerable<TlDescrip>> FilterDescripcion(string Limit, string Offset, string Descrip, string Fecha_upload);
        public Task<IEnumerable<TlDescrip>> FilterDescripcionFamilia(string Limit, string Offset, string Familia, string Fecha_upload);
        public Task<IEnumerable<TlDescrip>> FilterDescripcionSubFamilia(string Limit, string Offset, string Familia, string SubFamilia, string Fecha_upload);
        /*------------------------------------------------------------------*/
        public Task<IEnumerable<TlProvefilter>> FilterProveedorfilter(string Limit, string Offset, string ProveCod, string Fecha_upload);
        public Task<IEnumerable<TlProvefilter>> FilterProveedor(string Limit, string Offset, string Fecha_upload);
        public Task<IEnumerable<TlProvefilter>> ListadorProveedorAll(string Fecha_upload);
        public Task<IEnumerable<TlProvefilter>> FilterProveedorFamilia(string Limit, string Offset, string Familia, string Fecha_upload);
        public Task<IEnumerable<TlProvefilter>> FilterProveedorSubFamilia(string Limit, string Offset, string Familia, string SubFamilia, string Fecha_upload);
        /*------------------------------------------------------------------*/
        public Task<IEnumerable<TlArticuloAcumulado>> ListarArticuloInventarioFamilia(string Limit, string Offset, string Familia, string Fecha_upload);
        /*------------------------------------------------------------------*/
        public Task<IEnumerable<TlArticuloAcumulado>> ListarArticuloSubFamilia(string Limit, string Offset, string Familia, string SubFamilia, string Fecha_upload);
        /*------------------------------------------------------------------*/
        public Task<IEnumerable<TlCodiEqui>> FilterCodigoEquiFamilia(string Limit, string Offset, string Familia, string Fecha_upload);
        public Task<IEnumerable<TlCodiEqui>> FilterCodigoEquiSubFamilia(string Limit, string Offset, string Familia, string SubFamilia, string Fecha_upload);
        /*--------------------------------------------------------------------*/
        public Task<TdImportacion> DetalleImportacion(string Articulo);
    }
}
