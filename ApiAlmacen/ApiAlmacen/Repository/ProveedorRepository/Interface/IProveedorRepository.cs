using ApiAlmacen.Repository.ProveedorRepository.Models;

namespace ApiAlmacen.Repository.ProveedorRepository.Interface
{
    public interface IProveedorRepository
    {
        public Task<IEnumerable<TlProveedor>> ListadoProveedorAll(int Limit, int Offset);
        public Task<IEnumerable<TlProveedor>> ListadoProveedorCodigo(string Articulo, int Limit, int Offset);
        public Task<IEnumerable<TlProveedor>> ListadoProveedorFamiSubProv(string Familia, string SubFamilia, string Proveedor, int Limit, int Offset);
        /*---------------------------------------------------------------------------------------*/
        public Task<IEnumerable<TlngselectProve>> ListadoNgselectProveedor();
        public Task<IEnumerable<TlngselectProve>> ListadoNgSelectProveedorFamily(string Familia);
        public Task<IEnumerable<TlngselectProve>> ListadoNgSelectProveedorSubFamily(string Familia, string SubFamilia);
        /*---------------------------------------------------------------------------------------*/
        public Task<TlfamilyDescrip> DetailFamilia(string FamiliaCode);
        public Task<TlsubfamilyDescrip> DetailSubFamilia(string FamiliaCode, string SubFamiliaCode);
        public Task<TlNombreProve> DetailProveedor(string ProveedorCode);
    }
}
