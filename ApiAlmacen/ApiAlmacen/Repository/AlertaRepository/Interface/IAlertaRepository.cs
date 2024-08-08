using ApiAlmacen.Repository.AlertaRepository.Models;

namespace ApiAlmacen.Repository.AlertaRepository.Interface
{
    public interface IAlertaRepository
    {
        public Task<IEnumerable<Tlcod>> BusquedaListadoAlerta(string Fecha_upload);
        public Task<IEnumerable<TlAlert>> ListadoaAlertaInventario(string Fecha_upload);
        public Task<IEnumerable<TlAlert>> StockAlertaInventario(string Fecha_upload);
        public Task<IEnumerable<TlAlert>> StockAlertaSinVenta(string Fecha_upload);
    }
}
