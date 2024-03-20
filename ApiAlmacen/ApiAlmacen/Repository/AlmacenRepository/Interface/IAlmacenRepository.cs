

using ApiAlmacen.Repository.AlmacenRepository.Models;

namespace ApiAlmacen.Repository.AlmacenRepository.Interface
{
    public interface IAlmacenRepository
    {
        public Task<IEnumerable<TlAlmacen>> ListarProductoAlmacen(string Articulo);
        public Task<IEnumerable<TlAlmacenBuscar>> ListarProductoCompletoAlmacen(string Articulo);
        public Task<IEnumerable<TlAlmacenBuscar>> ListarProductoCompletoAlmacen2();
    }
}
