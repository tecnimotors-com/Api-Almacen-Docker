

using ApiAlmacen.Repository.AlmacenRepository.Models;

namespace ApiAlmacen.Repository.AlmacenRepository.Interface
{
    public interface IAlmacenRepository
    {
        public Task<IEnumerable<TrAlmacen>> ListarProductoAlmacen(string Articulo);
        public Task<IEnumerable<TlAlmacenBuscar>> ListarProductoCompletoAlmacen(string Articulo);
        public Task<IEnumerable<TlAlmacenBuscar>> ListarProductoCompletoAlmacen2();
        public Task<bool> RegistrarAlmacen(TrAlmacen trAlmacen);
        public Task<IEnumerable<TlInventario>> ListarAnalisisCostosInventario(string Limit, string Offset);

        public Task<IEnumerable<TlFamilia>> ListarFamiliaCodigo();
        public Task<IEnumerable<TlSubFamilia>> ListarSubFamiliaCodigo(string Familia);
        public Task<IEnumerable<TlInventario>> ListarInventarioGeneral(string Limit, string Offset, string Familia, string SubFamilia);

        public Task<IEnumerable<TlArticulo>> ListarArticuloInventario(string Limit, string Offset);
        public Task<IEnumerable<TlArticulo>> ListarArticuloInventarioFilter(string Limit, string Offset, string Articulo);
        public Task<TlInventario> DetalleInventarioGeneral(string Articulo);
        public Task<TlMonthList> ListadoCountCantidad(string Articulo, string Mes1, string Mes2, string Mes3, string Mes4, string Mes5, string Mes6);
        public Task<IEnumerable<TlDetallelote>> ListarDetalleLoteImportacion(string Limit, string Offset, string Articulo);
        public Task<IEnumerable<TlDetallelote>> ListarDetalleLoteImportacionAll(string Articulo);
        public Task<TlDetLote> TotalDetalleLote(string Articulo, string Tc, string Desc, float Igv);
        public Task<TlPedidos> TotalPedidoAnalisis(string Articulo);
        /*------------------------------------------------------------------*/
        /*------------------------------------------------------------------*/
        /*------------------------------------------------------------------*/
        public Task<IEnumerable<TlArticulo>> FilterCodigoInterno(string Limit, string Offset, string CodiInte);
        /*------------------------------------------------------------------*/
        public Task<IEnumerable<TlCodiEqui>> FilterCodigoEqui(string Limit, string Offset, string CodigoEqui);
        /*------------------------------------------------------------------*/
        public Task<IEnumerable<TlDescrip>> FilterDescripcion(string Limit, string Offset, string Descrip);
	public Task<IEnumerable<TlDescrip>> FilterDescripcionFamilia(string Limit, string Offset, string Familia);
	public Task<IEnumerable<TlDescrip>> FilterDescripcionSubFamilia(string Limit, string Offset, string Familia , string SubFamilia);
        /*------------------------------------------------------------------*/
        public Task<IEnumerable<TlProvefilter>> FilterProveedorfilter(string Limit, string Offset, string ProveCod);
        public Task<IEnumerable<TlProvefilter>> FilterProveedor(string Limit, string Offset);
	public Task<IEnumerable<TlProvefilter>> FilterProveedorFamilia(string Limit, string Offset, string Familia);
	public Task<IEnumerable<TlProvefilter>> FilterProveedorSubFamilia(string Limit, string Offset, string Familia , string SubFamilia);
	/*------------------------------------------------------------------*/
	public Task<IEnumerable<TlArticulo>> ListarArticuloInventarioFamilia(string Limit, string Offset, string Familia);
	/*------------------------------------------------------------------*/
	public Task<IEnumerable<TlArticulo>> ListarArticuloSubFamilia(string Limit, string Offset, string Familia, string SubFamilia);
	/*------------------------------------------------------------------*/
	public Task<IEnumerable<TlCodiEqui>> FilterCodigoEquiFamilia(string Limit, string Offset, string Familia);
	public Task<IEnumerable<TlCodiEqui>> FilterCodigoEquiSubFamilia(string Limit, string Offset, string Familia , string SubFamilia);
    }
}
