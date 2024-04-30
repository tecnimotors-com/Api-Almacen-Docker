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
        public async Task<IEnumerable<TrAlmacen>> ListarProductoAlmacen(string Articulo)
        {
            var db = DbConnection();

            var sql = @"
			SELECT almacen, articulo, codigo_equivalente, descripcion, unid_medida, stock_actual, stock_comprometido, stock_transito, marca, familia, sub_familia, tipo, situacion
			FROM public.stock_almacen_2024_04_25 where articulo = '" + Articulo + "'";

            return await db.QueryAsync<TrAlmacen>(sql, new { articulo = Articulo });
        }

        public async Task<IEnumerable<TlAlmacenBuscar>> ListarProductoCompletoAlmacen(string Articulo)
        {
            var db = DbConnection();

            var sql = @"SELECT articulo FROM public.stock_almacen_2024_04_25 where articulo like '%" + Articulo + "%'";

            return await db.QueryAsync<TlAlmacenBuscar>(sql, new { articulo = Articulo });
        }

        public async Task<IEnumerable<TlAlmacenBuscar>> ListarProductoCompletoAlmacen2()
        {
            var db = DbConnection();

            var sql = @"SELECT articulo FROM public.stock_almacen_2024_04_25";

            return await db.QueryAsync<TlAlmacenBuscar>(sql, new { });
        }

        public async Task<bool> RegistrarAlmacen(TrAlmacen trAlmacen)
        {
            var db = DbConnection();
            var sql = @"
                        INSERT INTO public.stock_almacen_2024_04_25(
                        almacen, articulo, codigo_equivalente, descripcion, unid_medida, stock_actual, 
                        stock_comprometido, stock_transito, marca, familia, sub_familia, tipo, situacion)
                        VALUES (@Almacen, @Articulo, @Codigo_equivalente, @Descripcion, @Unid_medida, @Stock_actual, 
                        @Stock_comprometido, @Stock_transito, @Marca, @Familia, @Sub_familia, @Tipo, @Situacion);
                        ";

            var result = await db.ExecuteAsync(sql, new
            {
                trAlmacen.Almacen,
                trAlmacen.Articulo,
                trAlmacen.Codigo_equivalente,
                trAlmacen.Descripcion,
                trAlmacen.Unid_medida,
                trAlmacen.Stock_actual,
                trAlmacen.Stock_comprometido,
                trAlmacen.Stock_transito,
                trAlmacen.Marca,
                trAlmacen.Familia,
                trAlmacen.Sub_familia,
                trAlmacen.Tipo,
                trAlmacen.Situacion,
            });
            return result > 0;

        }
    }
}
