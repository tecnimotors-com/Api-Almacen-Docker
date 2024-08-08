using ApiAlmacen.Context;
using ApiAlmacen.Repository.ReporteRepository.Interface;
using ApiAlmacen.Repository.ReporteRepository.Models;
using Dapper;
using Npgsql;

namespace ApiAlmacen.Repository.ReporteRepository.Repo
{
    public class ReporteRepositories(PostgreSQLConfiguration connectionString, IConfiguration configuration) : IReporteRepository
    {
        private readonly PostgreSQLConfiguration _connectionString = connectionString;
        private readonly IConfiguration configuration = configuration;
        protected NpgsqlConnection DbConection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> RegistrarReporte(TrReporte trReporte)
        {
            var db = DbConection();
            var sql = @"
                        INSERT INTO public."+ configuration.GetValue<string>("BdProduccion:reporte_pedido")!+ @"(
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
                        FROM public."+ configuration.GetValue<string>("BdProduccion:reporte_pedido")!+ @" order by codigo asc;
                       ";
            return await db.QueryAsync<Tlreporte>(sql, new { });
        }

        public async Task<Tlreporte> ListarReporteFilter(string Articulo)
        {
            var db = DbConection();
            var sql = @"
			            SELECT idpedido, codigo, codigoequivalente, descripcion,
                        ajustesfecha, promes2024, promes2023, stockpedido, 
                        stockviaje, stockalma, mesesrese, pedidorecom, ordenpedido
                        FROM public."+ configuration.GetValue<string>("BdProduccion:reporte_pedido")!+ @" where codigo = '" + Articulo + @"'
                       ";

            var result = await db.QueryFirstOrDefaultAsync<Tlreporte>(sql, new { });
            return result!;
        }
        public async Task<bool> ActualizarAcumulados(TrReporte trReporte)
        {
            var db = DbConection();

            var sql = @"
                        UPDATE public."+ configuration.GetValue<string>("BdProduccion:reporte_pedido")!+ @"
	                    SET 
	                    codigo=@Codigo,
	                    codigoequivalente=@Codigoequivalente, 
	                    descripcion=@Descripcion, 
	                    ajustesfecha=@Ajustesfecha, 
	                    promes2024=@Promes2024, 
	                    promes2023=@Promes2023, 
	                    stockpedido=@Stockpedido, 
	                    stockviaje=@Stockviaje, 
	                    stockalma=@Stockalma,
	                    mesesrese=@Mesesrese, 
	                    pedidorecom=@Pedidorecom,
	                    ordenpedido=@Ordenpedido
	                    WHERE codigo = @Codigo
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
        /*  
    UPDATE public."+ configuration.GetValue<string>("BdProduccion:reporte_pedido")!+ @"
	SET 
	idpedido=?,
	codigo=?,
	codigoequivalente=?, 
	descripcion=?, 
	ajustesfecha=?, 
	promes2024=?, 
	promes2023=?, 
	stockpedido=?, 
	stockviaje=?, 
	stockalma=?,
	mesesrese=?, 
	pedidorecom=?,
	ordenpedido=?
	WHERE codigo = ''
         */
    }
}
