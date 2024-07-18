using ApiAlmacen.Context;
using ApiAlmacen.Repository.AdicionalesRepository.Interface;
using ApiAlmacen.Repository.AlmacenRepository.Models;
using ApiAlmacen.Repository.ProveedorRepository.Models;
using Dapper;
using Npgsql;

namespace ApiAlmacen.Repository.AdicionalesRepository.Repo
{
    public class AdicionalesRepositoryy(PostgreSQLConfiguration connectionString) : IAdicionalesRepository
    {
        private readonly PostgreSQLConfiguration _connectionString = connectionString;

        protected NpgsqlConnection DbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }
        public async Task<IEnumerable<TlProveedor>> ListarAnalisis(int Limit, int Offset)
        {
            var db = DbConnection();

            var sql = @"SELECT codigo_interno, codigo_equivalente, descripcion
            FROM public.analisis_inv_general_tecnimotors_2024_04_12 order by codigo_interno desc limit " + Limit + " offset " + Offset + " ;";

            return await db.QueryAsync<TlProveedor>(sql, new { });
        }
        public async Task<IEnumerable<TlProveedor>> ListarAnalisisFamilia(int Limit, int Offset, string Familia)
        {
            var db = DbConnection();

            var sql = @"SELECT codigo_interno, codigo_equivalente, descripcion FROM public.analisis_inv_general_tecnimotors_2024_04_12 
            where familia_codigo = '" + Familia + @"' 
            order by codigo_interno desc limit " + Limit + " offset " + Offset + " ;";

            return await db.QueryAsync<TlProveedor>(sql, new { });
        }
        public async Task<IEnumerable<TlProveedor>> ListarAnalisisFamiliaSubFamilia(int Limit, int Offset, string Familia, string SubFamilia)
        {
            var db = DbConnection();

            var sql = @"SELECT codigo_interno, codigo_equivalente, descripcion FROM public.analisis_inv_general_tecnimotors_2024_04_12 
            where familia_codigo = '" + Familia + @"' and sub_familia_codigo = '" + SubFamilia + @"' 
            order by codigo_interno desc limit " + Limit + " offset " + Offset + " ;";

            return await db.QueryAsync<TlProveedor>(sql, new { });
        }
        public async Task<IEnumerable<TlProveedor>> ListarAnalisisFamiliaSubFamiliaProveedor(int Limit, int Offset, string Familia, string SubFamilia, string ProveedorCodigo)
        {
            var db = DbConnection();

            var sql = @"SELECT codigo_interno, codigo_equivalente, descripcion FROM public.analisis_inv_general_tecnimotors_2024_04_12 
            where familia_codigo = '" + Familia + @"' and sub_familia_codigo = '" + SubFamilia + @"' and proveedor_nombre = '" + ProveedorCodigo + @"' 
            order by codigo_interno desc limit " + Limit + " offset " + Offset + " ;";

            return await db.QueryAsync<TlProveedor>(sql, new { });
        }
        public async Task<IEnumerable<TlProveedor>> ListarAnalisisProveedor(int Limit, int Offset, string ProveedorCodigo)
        {
            var db = DbConnection();

            var sql = @"SELECT codigo_interno, codigo_equivalente, descripcion FROM public.analisis_inv_general_tecnimotors_2024_04_12 
            where proveedor_nombre = '" + ProveedorCodigo + @"' 
            order by codigo_interno desc limit " + Limit + " offset " + Offset + " ;";

            return await db.QueryAsync<TlProveedor>(sql, new { });
        }
        public async Task<IEnumerable<TlProveedor>> ListarAnalisisFamiliaProveedor(int Limit, int Offset, string Familia, string ProveedorCodigo)
        {
            var db = DbConnection();

            var sql = @"SELECT codigo_interno, codigo_equivalente, descripcion FROM public.analisis_inv_general_tecnimotors_2024_04_12 
            where familia_codigo = '" + Familia + @"' and proveedor_nombre = '" + ProveedorCodigo + @"' 
            order by codigo_interno desc limit " + Limit + " offset " + Offset + " ;";

            return await db.QueryAsync<TlProveedor>(sql, new { });
        }
        /*------------------------------------------------------------------------------*/
        /*------------------------------------------------------------------------------*/
    }
}
