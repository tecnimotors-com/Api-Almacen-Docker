

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
    }
}
