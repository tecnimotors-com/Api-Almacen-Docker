using ApiAlmacen.Repository.AcumuladoRepository.Models;

namespace ApiAlmacen.Repository.AcumuladoRepository.Interface
{
    public interface IAcumuladoRepository
    {
        public Task<bool> RegistrarAcumulados(TrModels trmodels);
        public Task<TlfilterAcumulado> FiltroCodigoAcumulado(string Codigo);
        public Task<bool> ActualizarAcumulado(TaAcumulado acumu);
    }
}
