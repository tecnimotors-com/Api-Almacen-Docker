using ApiAlmacen.Context;
using ApiAlmacen.Repository.AlertaRepository.Interface;
using ApiAlmacen.Repository.AlertaRepository.Models;
using Dapper;
using Npgsql;

namespace ApiAlmacen.Repository.AlertaRepository.Repo
{
    public class AlertaRepository(PostgreSQLConfiguration connectionString) : IAlertaRepository
    {
        private readonly PostgreSQLConfiguration _connectionString = connectionString;

        protected NpgsqlConnection DbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<IEnumerable<Tlcod>> BusquedaListadoAlerta()
        {
            var db = DbConnection();

            var sql = @"
                        select codigo_interno From public.analisis_inv_general_tecnimotors_2024_04_12 where 
                        (cast(total_stock as decimal)/ cast(prom_venta_2024 as decimal)) < 4  and  prom_venta_2024 != '0'
                        --codigo_interno = 'CHN0017518' 
                        group by codigo_interno
                        --order by (cast(total_stock as decimal)/cast(prom_venta_2024 as decimal)) desc
                        --order by cast(prom_venta_2024 as decimal) desc
                        --order by cast(total_stock as int) desc
                        order by codigo_interno asc
                       ";

            return await db.QueryAsync<Tlcod>(sql, new { });
        }

        public async Task<IEnumerable<TlAlert>> ListadoaAlertaInventario()
        {
            var db = DbConnection();

            var sql = @"
                        select codigo_interno, codigo_equivalente, descripcion,total_stock ,prom_venta_2024, 
                        (cast(total_stock as decimal)/ cast(prom_venta_2024 as decimal)) as alertaInventario 
                        From public.analisis_inv_general_tecnimotors_2024_04_12 where 
                        (cast(total_stock as decimal)/ cast(prom_venta_2024 as decimal)) < 4  and  prom_venta_2024 != '0'
                        --codigo_interno = 'CHN0017518' 
                        group by codigo_interno, codigo_equivalente, descripcion ,alertaInventario, total_stock ,prom_venta_2024
                        --order by (cast(total_stock as decimal)/cast(prom_venta_2024 as decimal)) desc
                        --order by cast(prom_venta_2024 as decimal) desc
                        --order by cast(total_stock as int) desc
                        order by codigo_interno asc
                       ";

            return await db.QueryAsync<TlAlert>(sql, new { });
        }

        public async Task<IEnumerable<TlAlert>> StockAlertaInventario()
        {
            var db = DbConnection();
            var sql = @"
                        select codigo_interno, codigo_equivalente, descripcion, total_stock , prom_venta_2024, 
                        (cast(total_stock as decimal)/ cast(prom_venta_2024 as decimal)) as alertaInventario 
                        From public.analisis_inv_general_tecnimotors_2024_04_12 where 
                        (cast(total_stock as decimal)/ cast(prom_venta_2024 as decimal)) < 4  and  prom_venta_2024 != '0'
                        --codigo_interno = 'CHN0017518' 
                        group by codigo_interno, codigo_equivalente, descripcion ,alertaInventario, total_stock ,prom_venta_2024
                        --order by (cast(total_stock as decimal)/cast(prom_venta_2024 as decimal)) desc
                        --order by cast(prom_venta_2024 as decimal) desc
                        --order by cast(total_stock as int) desc
                        order by codigo_interno desc
                      ";
            return await db.QueryAsync<TlAlert>(sql, new { });
        }

        public async Task<IEnumerable<TlAlert>> StockAlertaSinVenta()
        {
            var db = DbConnection();
            var sql = @"
                        --QUERY: STOCK MAYOR A CERO SIN VENTAS
                        select codigo_interno, codigo_equivalente, descripcion, total_stock , prom_venta_2024, precio_vta_actual 
                        from public.analisis_inv_general_tecnimotors_2024_04_12 where prom_venta_2024 = '0' and 
                        codigo_equivalente is not null and descripcion is not null 
                        --codigo_interno = 'CHN0017518' 
                        --group by codigo_interno, codigo_equivalente, descripcion, total_stock , prom_venta_2024, precio_vta_actual 
                        --order by (cast(total_stock as decimal)/cast(prom_venta_2024 as decimal)) desc
                        --order by cast(prom_venta_2024 as decimal) desc
                        order by cast(total_stock as float) desc
                        --order by codigo_interno desc
                       ";
            return await db.QueryAsync<TlAlert>(sql, new { });
        }
    }
}
