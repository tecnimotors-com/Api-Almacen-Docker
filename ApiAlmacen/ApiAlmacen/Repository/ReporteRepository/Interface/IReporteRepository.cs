using ApiAlmacen.Repository.ReporteRepository.Models;

namespace ApiAlmacen.Repository.ReporteRepository.Interface
{
    public interface IReporteRepository
    {
        public Task<bool> RegistrarReporte(TrReporte trReporte);
        public Task<IEnumerable<Tlreporte>> ListadoReporte();
        public Task<IEnumerable<Tlreporte>> ListarReporteFilter(string Codigo);
    }
}
