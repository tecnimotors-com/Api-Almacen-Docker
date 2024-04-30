

using ApiAlmacen.Repository.AlmacenRepository.Models;

namespace ApiAlmacen.Repository.AlmacenRepository.Interface
{
    public interface IAlmacenRepository
    {
        public Task<IEnumerable<TrAlmacen>> ListarProductoAlmacen(string Articulo);
        public Task<IEnumerable<TlAlmacenBuscar>> ListarProductoCompletoAlmacen(string Articulo);
        public Task<IEnumerable<TlAlmacenBuscar>> ListarProductoCompletoAlmacen2();
        public Task<bool> RegistrarAlmacen(TrAlmacen trAlmacen);
    }
}
