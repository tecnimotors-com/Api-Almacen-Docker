using ApiAlmacen.Repository.ProveedorRepository.Models;

namespace ApiAlmacen.Repository.ProveedorRepository.Interface
{
    public interface IProveedorRepository
    {
        public Task<IEnumerable<TlProveedor>> ListadoProveedorAll(int Limit, int Offset, string Fecha_upload);
        public Task<IEnumerable<TlProveedor>> ListadoProveedorCodigo(string Articulo, int Limit, int Offset, string Fecha_upload);
        public Task<IEnumerable<TlProveedor>> ListadoProveedorFamiSubProv(string Familia, string SubFamilia, string Proveedor, int Limit, int Offset, string Fecha_upload);
        /*---------------------------------------------------------------------------------------*/
        public Task<IEnumerable<TlngselectProve>> ListadoNgselectProveedor(string Fecha_upload);
        public Task<IEnumerable<TlngselectProve>> ListadoNgSelectProveedorFamily(string Familia, string Fecha_upload);
        public Task<IEnumerable<TlngselectProve>> ListadoNgSelectProveedorSubFamily(string Familia, string SubFamilia, string Fecha_upload);
        /*---------------------------------------------------------------------------------------*/
        public Task<TlfamilyDescrip> DetailFamilia(string FamiliaCode, string Fecha_upload);
        public Task<TlsubfamilyDescrip> DetailSubFamilia(string FamiliaCode, string SubFamiliaCode, string Fecha_upload);
        public Task<TlNombreProve> DetailProveedor(string ProveedorCode, string Fecha_upload);
    }
}
