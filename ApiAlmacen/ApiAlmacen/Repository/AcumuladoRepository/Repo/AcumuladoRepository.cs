using ApiAlmacen.Context;
using ApiAlmacen.Repository.AcumuladoRepository.Interface;
using ApiAlmacen.Repository.AcumuladoRepository.Models;
using Dapper;
using Npgsql;

namespace ApiAlmacen.Repository.AcumuladoRepository.Repo
{
    public class AcumuladoRepository(PostgreSQLConfiguration connectionString, IConfiguration configuration) : IAcumuladoRepository
    {
        private readonly PostgreSQLConfiguration _connectionString = connectionString;
        private readonly IConfiguration configuration = configuration;
        protected NpgsqlConnection DbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }
        public async Task<bool> RegistrarAcumulados(TrModels trmodels)
        {
            var db = DbConnection();
            var sql = @"
                        INSERT INTO public."+ configuration.GetValue<string>("BdProduccion:analisis_importacion")!+ @"(
                    	codigo, fechaacumulado, nombrepedido, cantidadpedido,
                        nombreviajando, cantidadviajando)
	                    VALUES (@Codigo, @Fechaacumulado, @Nombrepedido,
                        @Cantidadpedido, @Nombreviajando, @Cantidadviajando);
                       ";

            var result = await db.ExecuteAsync(sql, new
            {
                trmodels.Codigo,
                trmodels.Fechaacumulado,
                trmodels.Nombrepedido,
                trmodels.Cantidadpedido,
                trmodels.Nombreviajando,
                trmodels.Cantidadviajando,
            });
            return result > 0;
        }
        public async Task<TlfilterAcumulado> FiltroCodigoAcumulado(string Codigo)
        {
            var db = DbConnection();
            var sql = @"
                        select codigo from public."+ configuration.GetValue<string>("BdProduccion:analisis_importacion")!+ @" where codigo = '" + Codigo + "'";

            var result = await db.QueryFirstOrDefaultAsync<TlfilterAcumulado>(sql, new { });
            return result!;
        }
        public async Task<bool> ActualizarAcumulado(TaAcumulado acumu)
        {
            var db = DbConnection();
            var sql = @"
                        UPDATE public."+ configuration.GetValue<string>("BdProduccion:analisis_importacion")!+ @"
	                    SET
                        fechaacumulado=@Fechaacumulado,
                        nombrepedido=@Nombrepedido,
                        cantidadpedido=@Cantidadpedido,
                        nombreviajando=@Nombreviajando, 
                        cantidadviajando=@Cantidadviajando
	                    WHERE codigo=@Codigo;
                              ";
            //actualización de catálogo
            var result = await db.ExecuteAsync(sql, new
            {
                acumu.Codigo,
                acumu.Fechaacumulado,
                acumu.Nombrepedido,
                acumu.Cantidadpedido,
                acumu.Nombreviajando,
                acumu.Cantidadviajando,
            });

            return result > 0;
        }
    }
}
