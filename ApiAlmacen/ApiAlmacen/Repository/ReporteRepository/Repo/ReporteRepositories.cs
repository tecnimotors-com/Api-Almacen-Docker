using ApiAlmacen.Context;
using ApiAlmacen.Repository.ReporteRepository.Interface;
using ApiAlmacen.Repository.ReporteRepository.Models;
using Dapper;
using Npgsql;

namespace ApiAlmacen.Repository.ReporteRepository.Repo
{
    public class ReporteRepositories(PostgreSQLConfiguration connectionString) : IReporteRepository
    {
        private readonly PostgreSQLConfiguration _connectionString = connectionString;
        protected NpgsqlConnection DbConection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> RegistrarReporte(TrReporte trReporte)
        {
            var db = DbConection();
            var sql = @"
                        INSERT INTO public.reportepedido(
	                    codigo, codigoequivalente, descripcion, ajustesfecha, promes2024, promes2023, 
                        stockpedido, stockviaje, stockalma, mesesrese, pedidorecom, ordenpedido)
	                    VALUES ( @Codigo, @Codigoequivalente, @Descripcion, @Ajustesfecha, @Promes2024, @Promes2023, 
                        @Stockpedido, @Stockviaje, @Stockalma, @Mesesrese, @Pedidorecom, @Ordenpedido);
                       ";
            var result = await db.ExecuteAsync(sql, new
            {
                trReporte.Idpedido,
                trReporte.Codigo,
                trReporte.Codigoequivalente,
                trReporte.Descripcion,
                trReporte.Ajustesfecha,
                trReporte.Promes2024,
                trReporte.Promes2023,
                trReporte.Stockpedido,
                trReporte.Stockviaje,
                trReporte.Stockalma,
                trReporte.Mesesrese,
                trReporte.Pedidorecom,
                trReporte.Ordenpedido,
            });
            return result > 0;
        }

        public async Task<IEnumerable<Tlreporte>> ListadoReporte()
        {
            var db = DbConection();
            var sql = @"
                        SELECT idpedido, codigo, codigoequivalente, descripcion,
                        ajustesfecha, promes2024, promes2023, stockpedido, 
                        stockviaje, stockalma, mesesrese, pedidorecom, ordenpedido
                        FROM public.reportepedido;
                       ";
            return await db.QueryAsync<Tlreporte>(sql, new { });
        }

        public async Task<IEnumerable<Tlreporte>> ListarReporteFilter(string Codigo)
        {
            var db = DbConection();
            var sql = @"
                        SELECT idpedido, codigo, codigoequivalente, descripcion,
                        ajustesfecha, promes2024, promes2023, stockpedido, 
                        stockviaje, stockalma, mesesrese, pedidorecom, ordenpedido
                        FROM public.reportepedido where codigo = '" + Codigo + @"';
                       ";

            return await db.QueryAsync<Tlreporte>(sql, new { });
        }
    }
}
