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
                        INSERT INTO public.analisis_inv_general_tecnimotors_2024_04_12(
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

        public async Task<IEnumerable<TlInventario>> ListarAnalisisCostosInventario(string Limit, string Offset)
        {
            var db = DbConnection();

            var sql = @"
	                    SELECT codigo_interno as codart, codigo_equivalente as codequi, descripcion, cantidad_solicitada, stock_viaje, stock_proy, 
total_stock, otros_motoline, stock_ate, stock_abtao, stock_brillantes, stock_iquitos, salidas_consumo, prom_venta_2024,
prom_venta_2023, total_ventas, valor_fob_ultimo, valor_fob_2019_antes, precio_vta_actual, precio_venta_2019, comprometido, 
ventas_2024_ate, ventas_2024_abtao, ventas_2024_brillantes, ventas_2024_iquitos, ventas_2023, ventas_2022, ventas_2021, 
ventas_2020, ventas_2019, ventas_2013, fecha_ult_venta, fecha_ult_compra, compra_2024_total, compra_2024_ultima, 
compra_2023_total, compra_2023_ultima, total_compra, stock_en_transito, transf_gratuita_del_mes, correccion_de_cod_tip_mov, 
correccion_de_cod_cantidad, valor_vta_soles, valor_vta_dolar, ultimo_costo_prom_soles, ultimo_costo_prom_dolar, 
margen_unit_soles, margen_unit_dolar, margen_unitario, utilidad_por_ventas, abs_x_participacion, indice_rotacion, 
prom_ventas_ult_mes, nro_meses_stock, proveedor_nombre, proveedor_codigo, familia_codigo, familia_descripcion, 
sub_familia_codigo, sub_familia_descripcion
FROM public.analisis_inv_general_tecnimotors_2024_04_12 order by codart desc limit '" + Limit + "' offset '" + Offset + "' ;";

            return await db.QueryAsync<TlInventario>(sql, new { });
        }

        public async Task<IEnumerable<TlFamilia>> ListarFamiliaCodigo()
        {
            var db = DbConnection();
            var sql = @"
                        select distinct familia_codigo, familia_descripcion
                        from public.analisis_inv_general_tecnimotors_2024_04_12 
                        WHERE familia_codigo IS NOT NULL order by familia_codigo desc
                       ";
            return await db.QueryAsync<TlFamilia>(sql, new { });
        }
        public async Task<IEnumerable<TlSubFamilia>> ListarSubFamiliaCodigo(string Familia)
        {
            var db = DbConnection();
            var sql = @"
                        select distinct sub_familia_codigo, sub_familia_descripcion
                        from public.analisis_inv_general_tecnimotors_2024_04_12 
                        WHERE familia_codigo = '" + Familia + "' and sub_familia_codigo IS NOT NULL order by sub_familia_codigo asc";

            return await db.QueryAsync<TlSubFamilia>(sql, new { });
        }

        public async Task<IEnumerable<TlInventario>> ListarInventarioGeneral(string Limit, string Offset, string Familia, string SubFamilia)
        {
            var db = DbConnection();

            var sql = @"
SELECT codigo_interno as codart, codigo_equivalente as codequi, descripcion, cantidad_solicitada, stock_viaje, stock_proy, 
total_stock, otros_motoline, stock_ate, stock_abtao, stock_brillantes, stock_iquitos, salidas_consumo, prom_venta_2024,
prom_venta_2023, total_ventas, valor_fob_ultimo, valor_fob_2019_antes, precio_vta_actual, precio_venta_2019, comprometido, 
ventas_2024_ate, ventas_2024_abtao, ventas_2024_brillantes, ventas_2024_iquitos, ventas_2023, ventas_2022, ventas_2021, 
ventas_2020, ventas_2019, ventas_2013, fecha_ult_venta, fecha_ult_compra, compra_2024_total, compra_2024_ultima, 
compra_2023_total, compra_2023_ultima, total_compra, stock_en_transito, transf_gratuita_del_mes, correccion_de_cod_tip_mov, 
correccion_de_cod_cantidad, valor_vta_soles, valor_vta_dolar, ultimo_costo_prom_soles, ultimo_costo_prom_dolar, 
margen_unit_soles, margen_unit_dolar, margen_unitario, utilidad_por_ventas, abs_x_participacion, indice_rotacion, 
prom_ventas_ult_mes, nro_meses_stock, proveedor_nombre, proveedor_codigo, familia_codigo, familia_descripcion, 
sub_familia_codigo, sub_familia_descripcion
FROM public.analisis_inv_general_tecnimotors_2024_04_12
                        where familia_codigo = '" + Familia + @"' and sub_familia_codigo = '" + SubFamilia + @"' 
                         order by codart desc limit '" + Limit + "' offset '" + Offset + "' ;";

            return await db.QueryAsync<TlInventario>(sql, new { });
        }


        public async Task<IEnumerable<TlArticulo>> ListarArticuloInventario(string Limit, string Offset)
        {
            var db = DbConnection();

            var sql = @"
                        SELECT distinct codigo_interno 
                        FROM public.analisis_inv_general_tecnimotors_2024_04_12 
                        order by codigo_interno desc 
                        limit '" + Limit + "' offset '" + Offset + "' ;";

            return await db.QueryAsync<TlArticulo>(sql, new { });
        }

        public async Task<IEnumerable<TlArticulo>> ListarArticuloInventarioFilter(string Limit, string Offset, string Articulo)
        {
            var db = DbConnection();

            var sql = @"
                        SELECT distinct codigo_interno 
                        FROM public.analisis_inv_general_tecnimotors_2024_04_12
                        where codigo_interno like upper('%" + Articulo + @"%')  
                        order by codigo_interno desc  
                        limit '" + Limit + "' offset '" + Offset + "' ;";

            return await db.QueryAsync<TlArticulo>(sql, new { });
        }

        public async Task<TlInventario> DetalleInventarioGeneral(string Articulo)
        {
            var db = DbConnection();

            var sql = @"
                        SELECT codigo_interno as codart, codigo_equivalente as codequi, descripcion, cantidad_solicitada, stock_viaje, stock_proy, 
                        total_stock, otros_motoline, stock_ate, stock_abtao, stock_brillantes, stock_iquitos, salidas_consumo, prom_venta_2024,
                        prom_venta_2023, total_ventas, valor_fob_ultimo, valor_fob_2019_antes, precio_vta_actual, precio_venta_2019, comprometido, 
                        ventas_2024_ate, ventas_2024_abtao, ventas_2024_brillantes, ventas_2024_iquitos, ventas_2023, ventas_2022, ventas_2021, 
                        ventas_2020, ventas_2019, ventas_2013, fecha_ult_venta, fecha_ult_compra, compra_2024_total, compra_2024_ultima, 
                        compra_2023_total, compra_2023_ultima, total_compra, stock_en_transito, transf_gratuita_del_mes, correccion_de_cod_tip_mov, 
                        correccion_de_cod_cantidad, valor_vta_soles, valor_vta_dolar, ultimo_costo_prom_soles, ultimo_costo_prom_dolar, 
                        margen_unit_soles, margen_unit_dolar, margen_unitario, utilidad_por_ventas, abs_x_participacion, indice_rotacion, 
                        prom_ventas_ult_mes, nro_meses_stock, proveedor_nombre, proveedor_codigo, familia_codigo, familia_descripcion, 
                        sub_familia_codigo, sub_familia_descripcion
                        FROM public.analisis_inv_general_tecnimotors_2024_04_12
                        where codigo_interno = upper('" + Articulo + @"')
                      ;";

            return await db.QueryFirstOrDefaultAsync<TlInventario>(sql, new { codigo_interno = Articulo });
        }
    }
}
