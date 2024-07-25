using ApiAlmacen.Repository.AlertaRepository.Models;

namespace ApiAlmacen.Repository.AlertaRepository.Interface
{
    public interface IAlertaRepository
    {
        public Task<IEnumerable<Tlcod>> BusquedaListadoAlerta();
        public Task<IEnumerable<TlAlert>> ListadoaAlertaInventario();
        public Task<IEnumerable<TlAlert>> StockAlertaInventario();
        public Task<IEnumerable<TlAlert>> StockAlertaSinVenta();
    }
}
