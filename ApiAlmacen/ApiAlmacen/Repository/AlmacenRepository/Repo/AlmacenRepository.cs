using ApiAlmacen.Context;
using ApiAlmacen.Repository.AlmacenRepository.Interface;
using ApiAlmacen.Repository.AlmacenRepository.Models;
using Dapper;
using Npgsql;

namespace ApiAlmacen.Repository.AlmacenRepository.Repo
{
    public class AlmacenRepository : IAlmacenRepository
    {
        private readonly PostgreSQLConfiguration _connectionString;

        public AlmacenRepository(PostgreSQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }
        protected NpgsqlConnection DbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }
        public async Task<IEnumerable<TlAlmacen>> ListarProductoAlmacen(string Articulo)
        {
            var db = DbConnection();

            var sql = @"
                       SELECT articulo, marca, descripcion, unid_medida
                       FROM public.stock_almacen_2023_01_26 where articulo = '" + Articulo + "'";

            return await db.QueryAsync<TlAlmacen>(sql, new { articulo = Articulo });
        }

        public async Task<IEnumerable<TlAlmacenBuscar>> ListarProductoCompletoAlmacen(string Articulo)
        {
            var db = DbConnection();

            var sql = @"SELECT articulo FROM public.stock_almacen_2023_01_26 where articulo like '%" + Articulo + "%'";

            return await db.QueryAsync<TlAlmacenBuscar>(sql, new { articulo = Articulo });
        }

        public async Task<IEnumerable<TlAlmacenBuscar>> ListarProductoCompletoAlmacen2()
        {
            var db = DbConnection();

            var sql = @"SELECT articulo FROM public.stock_almacen_2023_01_26";

            return await db.QueryAsync<TlAlmacenBuscar>(sql, new {  });
        }
    }
}
