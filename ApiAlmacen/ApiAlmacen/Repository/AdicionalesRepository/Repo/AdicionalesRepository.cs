using ApiAlmacen.Context;
using ApiAlmacen.Repository.AdicionalesRepository.Interface;
using ApiAlmacen.Repository.ProveedorRepository.Models;
using Dapper;
using Npgsql;

namespace ApiAlmacen.Repository.AdicionalesRepository.Repo
{
    public class AdicionalesRepositoryy(PostgreSQLConfiguration connectionString, IConfiguration configuration) : IAdicionalesRepository
    {
        private readonly PostgreSQLConfiguration _connectionString = connectionString;
        private readonly IConfiguration configuration = configuration;
        protected NpgsqlConnection DbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        } 

        public async Task<IEnumerable<TlProveedor>> ListarAnalisis(int Limit, int Offset,string Fecha_upload)
        {
            var db = DbConnection();

            var sql = @"SELECT codigo_interno, codigo_equivalente, descripcion FROM public."+ configuration.GetValue<string>("BdProduccion:analisis_general")!+ @" 
                        where fecha_upload = '" + Fecha_upload + "' order by codigo_interno desc limit " + Limit + " offset " + Offset + " ;";

            return await db.QueryAsync<TlProveedor>(sql, new { });
        }
        public async Task<IEnumerable<TlProveedor>> ListarAnalisisFamilia(int Limit, int Offset, string Familia,string Fecha_upload)
        {
            var db = DbConnection();

            var sql = @"SELECT codigo_interno, codigo_equivalente, descripcion FROM public."+ configuration.GetValue<string>("BdProduccion:analisis_general")!+ @" 
            where familia_codigo = '" + Familia + @"' and fecha_upload = '" + Fecha_upload + @"' 
            order by codigo_interno desc limit " + Limit + " offset " + Offset + " ;";

            return await db.QueryAsync<TlProveedor>(sql, new { });
        }
        public async Task<IEnumerable<TlProveedor>> ListarAnalisisFamiliaSubFamilia(int Limit, int Offset, string Familia, string SubFamilia, string Fecha_upload)
        {
            var db = DbConnection();

            var sql = @"SELECT codigo_interno, codigo_equivalente, descripcion FROM public."+ configuration.GetValue<string>("BdProduccion:analisis_general")!+ @" 
            where familia_codigo = '" + Familia + @"' and sub_familia_codigo = '" + SubFamilia + @"' and fecha_upload = '" + Fecha_upload + @"' 
            order by codigo_interno desc limit " + Limit + " offset " + Offset + " ;";

            return await db.QueryAsync<TlProveedor>(sql, new { });
        }
        public async Task<IEnumerable<TlProveedor>> ListarAnalisisFamiliaSubFamiliaProveedor(int Limit, int Offset, string Familia, string SubFamilia, string ProveedorCodigo, string Fecha_upload)
        {
            var db = DbConnection();

            var sql = @"SELECT codigo_interno, codigo_equivalente, descripcion FROM public."+ configuration.GetValue<string>("BdProduccion:analisis_general")!+ @" 
            where familia_codigo = '" + Familia + @"' and sub_familia_codigo = '" + SubFamilia + @"' and proveedor_nombre = '" + ProveedorCodigo + @"' and fecha_upload = '" + Fecha_upload + @"' 
            order by codigo_interno desc limit " + Limit + " offset " + Offset + " ;";

            return await db.QueryAsync<TlProveedor>(sql, new { });
        }
        public async Task<IEnumerable<TlProveedor>> ListarAnalisisProveedor(int Limit, int Offset, string ProveedorCodigo, string Fecha_upload)
        {
            var db = DbConnection();

            var sql = @"SELECT codigo_interno, codigo_equivalente, descripcion FROM public."+ configuration.GetValue<string>("BdProduccion:analisis_general")!+ @" 
            where proveedor_nombre = '" + ProveedorCodigo + @"' and fecha_upload = '" + Fecha_upload + @"' 
            order by codigo_interno desc limit " + Limit + " offset " + Offset + " ;";

            return await db.QueryAsync<TlProveedor>(sql, new { });
        }
        public async Task<IEnumerable<TlProveedor>> ListarAnalisisFamiliaProveedor(int Limit, int Offset, string Familia, string ProveedorCodigo, string Fecha_upload)
        {
            var db = DbConnection();

            var sql = @"SELECT codigo_interno, codigo_equivalente, descripcion FROM public."+ configuration.GetValue<string>("BdProduccion:analisis_general")!+ @" 
            where familia_codigo = '" + Familia + @"' and proveedor_nombre = '" + ProveedorCodigo + @"' and fecha_upload = '" + Fecha_upload + @"' 
            order by codigo_interno desc limit " + Limit + " offset " + Offset + " ;";

            return await db.QueryAsync<TlProveedor>(sql, new { });
        }
        /*------------------------------------------------------------------------------*/
        /*------------------------------------------------------------------------------*/
    }
}
