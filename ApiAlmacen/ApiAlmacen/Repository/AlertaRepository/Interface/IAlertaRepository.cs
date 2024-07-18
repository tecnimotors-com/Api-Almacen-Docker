using ApiAlmacen.Repository.AlertaRepository.Models;

namespace ApiAlmacen.Repository.AlertaRepository.Interface
{
    public interface IAlertaRepository
    {
        public Task<IEnumerable<Tlcod>> BusquedaListadoAlerta();
        public Task<IEnumerable<TlAlert>> ListadoaAlertaInventario();
    }
}
