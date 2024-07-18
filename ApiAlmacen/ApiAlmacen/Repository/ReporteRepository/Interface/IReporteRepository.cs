using ApiAlmacen.Repository.ReporteRepository.Models;

namespace ApiAlmacen.Repository.ReporteRepository.Interface
{
    public interface IReporteRepository
    {
        public Task<bool> RegistrarReporte(TrReporte trReporte);
        public Task<IEnumerable<Tlreporte>> ListadoReporte();
        public Task<Tlreporte> ListarReporteFilter(string Articulo);
        public Task<bool> ActualizarAcumulados(TrReporte trReporte);
    }
}
