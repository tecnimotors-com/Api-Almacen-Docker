using ApiAlmacen.Repository.ProveedorRepository.Models;

namespace ApiAlmacen.Repository.AdicionalesRepository.Interface
{
    public interface IAdicionalesRepository
    {
        /*------------------------------------------------------------------------------*/
        /*------------------------------------------------------------------------------*/
        public Task<IEnumerable<TlProveedor>> ListarAnalisis(
            int Limit, int Offset);
        public Task<IEnumerable<TlProveedor>> ListarAnalisisFamilia(
            int Limit, int Offset, string Familia);
        public Task<IEnumerable<TlProveedor>> ListarAnalisisFamiliaSubFamilia(
            int Limit, int Offset, string Familia, string SubFamilia);
        public Task<IEnumerable<TlProveedor>> ListarAnalisisFamiliaSubFamiliaProveedor(
            int Limit, int Offset, string Familia, string SubFamilia, string ProveedorCodigo);
        public Task<IEnumerable<TlProveedor>> ListarAnalisisProveedor(
            int Limit, int Offset, string ProveedorCodigo);
        public Task<IEnumerable<TlProveedor>> ListarAnalisisFamiliaProveedor(
        int Limit, int Offset, string Familia, string ProveedorCodigo);

        /*------------------------------------------------------------------------------*/
        /*------------------------------------------------------------------------------*/
    }
}
