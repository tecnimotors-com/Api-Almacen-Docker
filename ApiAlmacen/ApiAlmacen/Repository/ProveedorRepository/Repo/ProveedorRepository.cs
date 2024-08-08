using ApiAlmacen.Context;
using ApiAlmacen.Repository.ProveedorRepository.Interface;
using ApiAlmacen.Repository.ProveedorRepository.Models;
using Dapper;
using Npgsql;

namespace ApiAlmacen.Repository.ProveedorRepository.Repo
{
    public class ProveedorRepository(PostgreSQLConfiguration connectionString, IConfiguration configuration) : IProveedorRepository
    {
        private readonly PostgreSQLConfiguration _connectionString = connectionString;
        private readonly IConfiguration configuration = configuration;

        protected NpgsqlConnection DbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }
        public async Task<IEnumerable<TlProveedor>> ListadoProveedorAll(int Limit, int Offset, string Fecha_upload)
        {
            var db = DbConnection();

            var sql = @"
                        select codigo_interno, codigo_equivalente, descripcion from public."+ configuration.GetValue<string>("BdProduccion:analisis_general")!+ @" 
                        where descripcion is not null and fecha_upload = '" + Fecha_upload + @"' 
                        order by codigo_interno desc 
                        limit " + Limit + " offset " + Offset;

            return await db.QueryAsync<TlProveedor>(sql, new { });
        }
        public async Task<IEnumerable<TlProveedor>> ListadoProveedorCodigo(string Articulo, int Limit, int Offset, string Fecha_upload)
        {
            var db = DbConnection();

            var sql = @"
                        select codigo_interno, codigo_equivalente, descripcion from public."+ configuration.GetValue<string>("BdProduccion:analisis_general")!+ @"
                        where proveedor_nombre= '" + Articulo + @"' and descripcion is not null and fecha_upload = '" + Fecha_upload + @"' 
                        order by codigo_interno desc 
                        limit " + Limit + " offset " + Offset;

            return await db.QueryAsync<TlProveedor>(sql, new { articulo = Articulo });
        }
        public async Task<IEnumerable<TlProveedor>> ListadoProveedorFamiSubProv(string Familia, string SubFamilia, string Proveedor, int Limit, int Offset, string Fecha_upload)
        {
            var db = DbConnection();

            var sql = @"
                        select codigo_interno, codigo_equivalente, descripcion from public."+ configuration.GetValue<string>("BdProduccion:analisis_general")!+ @"
                        where  familia_codigo = '" + Familia + @"' and sub_familia_codigo = '" + SubFamilia + @"' and proveedor_nombre= '" + Proveedor + @"' and descripcion is not null and fecha_upload = '" + Fecha_upload + @"' 
                        order by codigo_interno desc 
                        limit " + Limit + " offset " + Offset;

            return await db.QueryAsync<TlProveedor>(sql, new { });
        }
        /*---------------------------------------------------------------------------------------*/
        public async Task<IEnumerable<TlngselectProve>> ListadoNgselectProveedor(string Fecha_upload)
        {
            var db = DbConnection();
            var sql = @"
                        SELECT distinct proveedor_nombre as proveedor_codigo, proveedor_codigo as proveedor_nombre
                        FROM public."+ configuration.GetValue<string>("BdProduccion:analisis_general")!+ @"
                        where  proveedor_nombre IS NOT NULL and proveedor_codigo IS NOT NULL and fecha_upload = '" + Fecha_upload + @"' 
                        order by proveedor_nombre asc
                       ";
            return await db.QueryAsync<TlngselectProve>(sql, new { });
        }
        public async Task<IEnumerable<TlngselectProve>> ListadoNgSelectProveedorFamily(string Familia, string Fecha_upload)
        {
            var db = DbConnection();
            var sql = @"
                        SELECT distinct proveedor_nombre as proveedor_codigo, proveedor_codigo as proveedor_nombre
                        FROM public."+ configuration.GetValue<string>("BdProduccion:analisis_general")!+ @"
                        where familia_codigo = '" + Familia + @"' and proveedor_nombre IS NOT NULL and proveedor_codigo IS NOT NULL and fecha_upload = '" + Fecha_upload + @"'  
                        order by proveedor_nombre asc
                       ";
            return await db.QueryAsync<TlngselectProve>(sql, new { });
        }
        public async Task<IEnumerable<TlngselectProve>> ListadoNgSelectProveedorSubFamily(string Familia, string SubFamilia, string Fecha_upload)
        {
            var db = DbConnection();
            var sql = @"
                        SELECT distinct proveedor_nombre as proveedor_codigo, proveedor_codigo as proveedor_nombre 
                        FROM public."+ configuration.GetValue<string>("BdProduccion:analisis_general")!+ @" 
                        where familia_codigo = '" + Familia + @"' and sub_familia_codigo = '" + SubFamilia + @"' and 
                        proveedor_nombre IS NOT NULL and proveedor_codigo IS NOT NULL and fecha_upload = '" + Fecha_upload + @"' 
                        order by proveedor_nombre asc 
                       ";
            return await db.QueryAsync<TlngselectProve>(sql, new { });
        }
        /*---------------------------------------------------------------------------------------*/
        public async Task<TlfamilyDescrip> DetailFamilia(string FamiliaCode, string Fecha_upload)
        {
            var db = DbConnection();
            var sql = @"
                        select distinct lower(familia_descripcion) as familiadescripcion from 
                        public."+ configuration.GetValue<string>("BdProduccion:analisis_general")!+ @" 
                        where familia_codigo= '" + FamiliaCode + @"' and familia_codigo is not null and fecha_upload = '" + Fecha_upload + @"' 
                       ";
            var result = await db.QueryFirstOrDefaultAsync<TlfamilyDescrip>(sql, new { });
            return result!;
        }
        public async Task<TlsubfamilyDescrip> DetailSubFamilia(string FamiliaCode, string SubFamiliaCode, string Fecha_upload)
        {
            var db = DbConnection();
            var sql = @"
                       select distinct lower(sub_familia_descripcion) as subfamiliadescripcion
                        from public."+ configuration.GetValue<string>("BdProduccion:analisis_general")!+ @" 
                        where familia_codigo= '" + FamiliaCode + @"' and sub_familia_codigo = '" + SubFamiliaCode + @"' and fecha_upload = '" + Fecha_upload + @"' 
                        and sub_familia_codigo is not null 
                       ";
            var result = await db.QueryFirstOrDefaultAsync<TlsubfamilyDescrip>(sql, new { });
            return result!;
        }
        public async Task<TlNombreProve> DetailProveedor(string ProveedorCode, string Fecha_upload)
        {
            var db = DbConnection();
            var sql = @"
                        select distinct lower(proveedor_codigo) as nombreproveedor from 
                        public."+ configuration.GetValue<string>("BdProduccion:analisis_general")!+ @"
                        where proveedor_nombre = '" + ProveedorCode + @"' and proveedor_nombre is not null and fecha_upload = '" + Fecha_upload + @"' 
                       ";
            var result = await db.QueryFirstOrDefaultAsync<TlNombreProve>(sql, new { });
            return result!;
        }
    }
}
