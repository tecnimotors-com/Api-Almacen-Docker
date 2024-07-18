using ApiAlmacen.Context;
using ApiAlmacen.Repository.ProveedorRepository.Interface;
using ApiAlmacen.Repository.ProveedorRepository.Models;
using Dapper;
using Npgsql;

namespace ApiAlmacen.Repository.ProveedorRepository.Repo
{
    public class ProveedorRepository(PostgreSQLConfiguration connectionString) : IProveedorRepository
    {
        private readonly PostgreSQLConfiguration _connectionString = connectionString;

        protected NpgsqlConnection DbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }
        public async Task<IEnumerable<TlProveedor>> ListadoProveedorAll(int Limit, int Offset)
        {
            var db = DbConnection();

            var sql = @"
                        select codigo_interno, codigo_equivalente, descripcion from public.analisis_inv_general_tecnimotors_2024_04_12 
                        where descripcion is not null 
                        order by codigo_interno desc 
                        limit " + Limit + " offset " + Offset;

            return await db.QueryAsync<TlProveedor>(sql, new { });
        }
        public async Task<IEnumerable<TlProveedor>> ListadoProveedorCodigo(string Articulo, int Limit, int Offset)
        {
            var db = DbConnection();

            var sql = @"
                        select codigo_interno, codigo_equivalente, descripcion from public.analisis_inv_general_tecnimotors_2024_04_12
                        where proveedor_nombre= '" + Articulo + @"' and descripcion is not null 
                        order by codigo_interno desc 
                        limit " + Limit + " offset " + Offset;

            return await db.QueryAsync<TlProveedor>(sql, new { articulo = Articulo });
        }
        public async Task<IEnumerable<TlProveedor>> ListadoProveedorFamiSubProv(string Familia, string SubFamilia, string Proveedor, int Limit, int Offset)
        {
            var db = DbConnection();

            var sql = @"
                        select codigo_interno, codigo_equivalente, descripcion from public.analisis_inv_general_tecnimotors_2024_04_12
                        where  familia_codigo = '" + Familia + @"' and sub_familia_codigo = '" + SubFamilia + @"' and proveedor_nombre= '" + Proveedor + @"' and descripcion is not null 
                        order by codigo_interno desc 
                        limit " + Limit + " offset " + Offset;

            return await db.QueryAsync<TlProveedor>(sql, new { });
        }
        /*---------------------------------------------------------------------------------------*/
        public async Task<IEnumerable<TlngselectProve>> ListadoNgselectProveedor()
        {
            var db = DbConnection();
            var sql = @"
                        SELECT distinct proveedor_nombre as proveedor_codigo, proveedor_codigo as proveedor_nombre
                        FROM public.analisis_inv_general_tecnimotors_2024_04_12
                        where  proveedor_nombre IS NOT NULL and proveedor_codigo IS NOT NULL
                        order by proveedor_nombre asc
                       ";
            return await db.QueryAsync<TlngselectProve>(sql, new { });
        }
        public async Task<IEnumerable<TlngselectProve>> ListadoNgSelectProveedorFamily(string Familia)
        {
            var db = DbConnection();
            var sql = @"
                        SELECT distinct proveedor_nombre as proveedor_codigo, proveedor_codigo as proveedor_nombre
                        FROM public.analisis_inv_general_tecnimotors_2024_04_12
                        where familia_codigo = '" + Familia + @"' and proveedor_nombre IS NOT NULL and proveedor_codigo IS NOT NULL
                        order by proveedor_nombre asc
                       ";
            return await db.QueryAsync<TlngselectProve>(sql, new { });
        }
        public async Task<IEnumerable<TlngselectProve>> ListadoNgSelectProveedorSubFamily(string Familia, string SubFamilia)
        {
            var db = DbConnection();
            var sql = @"
                        SELECT distinct proveedor_nombre as proveedor_codigo, proveedor_codigo as proveedor_nombre 
                        FROM public.analisis_inv_general_tecnimotors_2024_04_12 
                        where familia_codigo = '" + Familia + @"' and sub_familia_codigo = '" + SubFamilia + @"' and 
                        proveedor_nombre IS NOT NULL and proveedor_codigo IS NOT NULL 
                        order by proveedor_nombre asc 
                       ";
            return await db.QueryAsync<TlngselectProve>(sql, new { });
        }
        /*---------------------------------------------------------------------------------------*/
        public async Task<TlfamilyDescrip> DetailFamilia(string FamiliaCode)
        {
            var db = DbConnection();
            var sql = @"
                        select distinct lower(familia_descripcion) as familiadescripcion from 
                        public.analisis_inv_general_tecnimotors_2024_04_12 
                        where familia_codigo= '" + FamiliaCode + @"' and familia_codigo is not null
                       ";
            var result = await db.QueryFirstOrDefaultAsync<TlfamilyDescrip>(sql, new { });
            return result!;
        }
        public async Task<TlsubfamilyDescrip> DetailSubFamilia(string FamiliaCode, string SubFamiliaCode)
        {
            var db = DbConnection();
            var sql = @"
                       select distinct lower(sub_familia_descripcion) as subfamiliadescripcion
                        from public.analisis_inv_general_tecnimotors_2024_04_12 
                        where familia_codigo= '" + FamiliaCode + @"' and sub_familia_codigo = '" + SubFamiliaCode + @"' 
                        and sub_familia_codigo is not null 
                       ";
            var result = await db.QueryFirstOrDefaultAsync<TlsubfamilyDescrip>(sql, new { });
            return result!;
        }
        public async Task<TlNombreProve> DetailProveedor(string ProveedorCode)
        {
            var db = DbConnection();
            var sql = @"
                        select distinct lower(proveedor_codigo) as nombreproveedor from 
                        public.analisis_inv_general_tecnimotors_2024_04_12
                        where proveedor_nombre = '" + ProveedorCode + @"' and proveedor_nombre is not null
                       ";
            var result = await db.QueryFirstOrDefaultAsync<TlNombreProve>(sql, new { });
            return result!;
        }
    }
}
