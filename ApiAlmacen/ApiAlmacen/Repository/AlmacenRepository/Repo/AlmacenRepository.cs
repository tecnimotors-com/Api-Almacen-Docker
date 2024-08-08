using ApiAlmacen.Context;
using ApiAlmacen.Repository.AlmacenRepository.Interface;
using ApiAlmacen.Repository.AlmacenRepository.Models;
using Dapper;
using Npgsql;

namespace ApiAlmacen.Repository.AlmacenRepository.Repo
{
    public class AlmacenRepository(PostgreSQLConfiguration connectionString, IConfiguration configuration) : IAlmacenRepository
    {
        private readonly PostgreSQLConfiguration _connectionString = connectionString;
        private readonly IConfiguration configuration = configuration;
        protected NpgsqlConnection DbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }
        
        public async Task<IEnumerable<TrAlmacen>> ListarProductoAlmacen(string Articulo)
        {
            var db = DbConnection();
            var sql = @"
			SELECT almacen, articulo, codigo_equivalente, descripcion, unid_medida, stock_actual, stock_comprometido, stock_transito, marca, familia, sub_familia, tipo, situacion
			FROM public."+ configuration.GetValue<string>("BdProduccion:stock_almacen")!+ @" where articulo = '" + Articulo + "'";

            return await db.QueryAsync<TrAlmacen>(sql, new { articulo = Articulo });
        }

        public async Task<IEnumerable<TlAlmacenBuscar>> ListarProductoCompletoAlmacen(string Articulo)
        {
            var db = DbConnection();

            var sql = @"SELECT articulo FROM public."+ configuration.GetValue<string>("BdProduccion:stock_almacen")!+ @" where articulo like '%" + Articulo + "%'";

            return await db.QueryAsync<TlAlmacenBuscar>(sql, new { articulo = Articulo });
        }

        public async Task<IEnumerable<TlAlmacenBuscar>> ListarProductoCompletoAlmacen2()
        {
            var db = DbConnection();

            var sql = @"SELECT articulo FROM public."+ configuration.GetValue<string>("BdProduccion:stock_almacen")!;

            return await db.QueryAsync<TlAlmacenBuscar>(sql, new { });
        }

        public async Task<bool> RegistrarAlmacen(TrAlmacen trAlmacen)
        {
            var db = DbConnection();
            var sql = @"
                        INSERT INTO public."+ configuration.GetValue<string>("BdProduccion:analisis_general")!+ @"(
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

        public async Task<IEnumerable<TlInventario>> ListarAnalisisCostosInventario(string Limit, string Offset, string Fecha_upload)
        {
            var db = DbConnection();

            var sql = @"SELECT codigo_interno as codart, codigo_equivalente as codequi, descripcion, cantidad_solicitada, stock_viaje, stock_proy, 
            total_stock, otros_motoline, stock_ate, stock_abtao, stock_brillantes, stock_iquitos, salidas_consumo, prom_venta_2024,
            prom_venta_2023, total_ventas, valor_fob_ultimo, valor_fob_2019_antes, precio_vta_actual, precio_venta_2019, comprometido, 
            ventas_2024_ate, ventas_2024_abtao, ventas_2024_brillantes, ventas_2024_iquitos, ventas_2023, ventas_2022, ventas_2021, 
            ventas_2020, ventas_2019, ventas_2013, fecha_ult_venta, fecha_ult_compra, compra_2024_total, compra_2024_ultima, 
            compra_2023_total, compra_2023_ultima, total_compra, stock_en_transito, transf_gratuita_del_mes, correccion_de_cod_tip_mov, 
            correccion_de_cod_cantidad, valor_vta_soles, valor_vta_dolar, ultimo_costo_prom_soles, ultimo_costo_prom_dolar, 
            margen_unit_soles, margen_unit_dolar, margen_unitario, utilidad_por_ventas, abs_x_participacion, indice_rotacion, 
            prom_ventas_ult_mes, nro_meses_stock, proveedor_nombre, proveedor_codigo, familia_codigo, familia_descripcion, 
            sub_familia_codigo, sub_familia_descripcion
            FROM public."+ configuration.GetValue<string>("BdProduccion:analisis_general")!+"  where fecha_upload = '" + Fecha_upload + "' order by codart desc limit '" + Limit + "' offset '" + Offset + "' ;";

            return await db.QueryAsync<TlInventario>(sql, new { });
        }

        public async Task<IEnumerable<TlFamilia>> ListarFamiliaCodigo(string Fecha_upload)
        {
            var db = DbConnection();
            var sql = @"
                        select distinct familia_codigo, familia_descripcion
                        from public."+ configuration.GetValue<string>("BdProduccion:analisis_general")!+ @"  
                        WHERE familia_codigo IS NOT NULL and fecha_upload = '" + Fecha_upload + @"' order by familia_codigo desc
                       ";
            return await db.QueryAsync<TlFamilia>(sql, new { });
        }

        public async Task<IEnumerable<TlFamilia>> ListarFamiliaProveedor(string ProveedorCodigo, string Fecha_upload)
        {
            var db = DbConnection();
            var sql = @"select distinct familia_codigo, familia_descripcion
                    from public."+ configuration.GetValue<string>("BdProduccion:analisis_general")!+ @"  
                    WHERE familia_codigo IS NOT NULL and proveedor_nombre='" + ProveedorCodigo + @"' and fecha_upload = '" + Fecha_upload + @"' order by familia_codigo desc
                   ";
            return await db.QueryAsync<TlFamilia>(sql, new { });
        }
        
        public async Task<IEnumerable<TlSubFamilia>> ListarSubFamiliaCodigo(string Familia, string Fecha_upload)
        {
            var db = DbConnection();
            var sql = @"
                        select distinct sub_familia_codigo, sub_familia_descripcion
                        from public."+ configuration.GetValue<string>("BdProduccion:analisis_general")!+ @"  
                        WHERE familia_codigo = '" + Familia + "' and sub_familia_codigo IS NOT NULL  and fecha_upload = '" + Fecha_upload + @"' order by sub_familia_codigo asc";

            return await db.QueryAsync<TlSubFamilia>(sql, new { });
        }

        public async Task<IEnumerable<TlInventario>> ListarInventarioGeneral(string Limit, string Offset, string Familia, string SubFamilia, string Fecha_upload)
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
                        FROM public."+ configuration.GetValue<string>("BdProduccion:analisis_general")!+ @" 
                        where familia_codigo = '" + Familia + @"' and sub_familia_codigo = '" + SubFamilia + @"' and fecha_upload = '" + Fecha_upload + @"' 
                        order by codart desc limit '" + Limit + "' offset '" + Offset + "' ;";

            return await db.QueryAsync<TlInventario>(sql, new { });
        }
        
        /*------------------------------------------------------------------------------*/
        /*------------------------------------------------------------------------------*/
        
        public async Task<IEnumerable<TlArticulo>> ListarArticuloInventarioOnly(string Fecha_upload)
        {
            var db = DbConnection();

            var sql = @"
                        SELECT distinct codigo_interno 
                        FROM public."+ configuration.GetValue<string>("BdProduccion:analisis_general")!+ @" 
                        where fecha_upload = '" + Fecha_upload + @"' 
                        order by codigo_interno desc ";

            return await db.QueryAsync<TlArticulo>(sql, new { });
        }
       
        public async Task<IEnumerable<TlArticulo>> ListarEquivalentoInventarioOnly(string Fecha_upload)
        {
            var db = DbConnection();

            var sql = @"
                        SELECT distinct codigo_interno, codigo_equivalente
                        FROM public."+ configuration.GetValue<string>("BdProduccion:analisis_general")!+ @" 
                        where fecha_upload = '" + Fecha_upload + @"' 
                        order by codigo_interno desc ";

            return await db.QueryAsync<TlArticulo>(sql, new { });
        }
        
        public async Task<IEnumerable<TlArticulo>> ListarDescripcionInventariOnly(string Fecha_upload)
        {
            var db = DbConnection();

            var sql = @"
                        SELECT distinct codigo_interno, codigo_equivalente, descripcion
                        FROM public."+ configuration.GetValue<string>("BdProduccion:analisis_general")!+ @"  
                        where fecha_upload = '" + Fecha_upload + @"' 
                        order by codigo_interno desc ";

            return await db.QueryAsync<TlArticulo>(sql, new { });
        }
        
        /*------------------------------------------------------------------------------*/
        
        public async Task<IEnumerable<TlArticuloAcumulado>> ListarArticuloInventario(string Limit, string Offset, string Fecha_upload)
        {
            var db = DbConnection();

            var sql = @"
                        SELECT distinct codigo_interno, codigo_equivalente, descripcion
                        FROM public."+ configuration.GetValue<string>("BdProduccion:analisis_general")!+ @"  
                        where fecha_upload = '" + Fecha_upload + @"' 
                        order by codigo_interno desc 
                        limit '" + Limit + "' offset '" + Offset + "' ;";

            return await db.QueryAsync<TlArticuloAcumulado>(sql, new { });
        }
        
        public async Task<IEnumerable<TlCodiEqui>> ListarEquivalentoInventario(string Limit, string Offset, string Fecha_upload)
        {
            var db = DbConnection();

            var sql = @"
                        SELECT distinct codigo_interno, codigo_equivalente
                        FROM public."+ configuration.GetValue<string>("BdProduccion:analisis_general")!+ @"  
                        where fecha_upload = '" + Fecha_upload + @"' 
                        order by codigo_interno desc 
                        limit '" + Limit + "' offset '" + Offset + "' ;";

            return await db.QueryAsync<TlCodiEqui>(sql, new { });
        }
        
        public async Task<IEnumerable<TlDescrip>> ListarDescripcionInventario(string Limit, string Offset, string Fecha_upload)
        {
            var db = DbConnection();

            var sql = @"
                        SELECT distinct codigo_interno, codigo_equivalente, descripcion
                        FROM public."+ configuration.GetValue<string>("BdProduccion:analisis_general")!+ @"  
                        where fecha_upload = '" + Fecha_upload + @"' 
                        order by codigo_interno desc 
                        limit '" + Limit + "' offset '" + Offset + "' ;";

            return await db.QueryAsync<TlDescrip>(sql, new { });
        }
        
        /*------------------------------------------------------------------------------*/
        /*------------------------------------------------------------------------------*/
        /*------------------------------------------------------------------------------*/
        
        public async Task<IEnumerable<TlArticuloAcumulado>> ListarArticuloInventarioFamilia(string Limit, string Offset, string Familia, string Fecha_upload)
        {
            var db = DbConnection();

            var sql = @"
                        SELECT distinct codigo_interno, codigo_equivalente, descripcion
                        FROM public."+ configuration.GetValue<string>("BdProduccion:analisis_general")!+ @"  
            			where familia_codigo = '" + Familia + @"' and fecha_upload = '" + Fecha_upload + @"' 
                        order by codigo_interno desc 
                        limit '" + Limit + "' offset '" + Offset + "' ;";

            return await db.QueryAsync<TlArticuloAcumulado>(sql, new { });
        }

        public async Task<IEnumerable<TlArticuloAcumulado>> ListarArticuloSubFamilia(string Limit, string Offset, string Familia, string SubFamilia, string Fecha_upload)
        {
            var db = DbConnection();

            var sql = @"
                        SELECT distinct codigo_interno, codigo_equivalente, descripcion
                        FROM public."+ configuration.GetValue<string>("BdProduccion:analisis_general")!+ @"  
			            where familia_codigo = '" + Familia + "' and sub_familia_codigo = '" + SubFamilia + @"' and fecha_upload = '" + Fecha_upload + @"' 
                        order by codigo_interno desc 
                        limit '" + Limit + "' offset '" + Offset + "' ;";

            return await db.QueryAsync<TlArticuloAcumulado>(sql, new { });
        }

        public async Task<IEnumerable<TlArticuloAcumulado>> ListarArticuloInventarioFilter(string Limit, string Offset, string Articulo, string Fecha_upload)
        {
            var db = DbConnection();

            var sql = @"
                        SELECT distinct codigo_interno, codigo_equivalente, descripcion
                        FROM public."+ configuration.GetValue<string>("BdProduccion:analisis_general")!+ @" 
                        where codigo_interno like upper('%" + Articulo + @"%') and fecha_upload = '" + Fecha_upload + @"' 
                        order by codigo_interno desc  
                        limit '" + Limit + "' offset '" + Offset + "' ;";

            return await db.QueryAsync<TlArticuloAcumulado>(sql, new { });
        }

        public async Task<TlInventario> DetalleInventarioGeneral(string Articulo, string Fecha_upload)
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
                        FROM public."+ configuration.GetValue<string>("BdProduccion:analisis_general")!+ @" 
                        where codigo_interno = upper('" + Articulo + @"') and fecha_upload = '" + Fecha_upload + @"' 
                      ;";

            var result = await db.QueryFirstOrDefaultAsync<TlInventario>(sql, new { codigo_interno = Articulo });
            return result!;
        }

        public async Task<TlMonthList> ListadoCountCantidad(string Articulo, string Mes1, string Mes2, string Mes3, string Mes4, string Mes5, string Mes6)
        {
            var month1 = Mes1.Replace('-', '/');
            var month2 = Mes2.Replace('-', '/');
            var month3 = Mes3.Replace('-', '/');
            var month4 = Mes4.Replace('-', '/');
            var month5 = Mes5.Replace('-', '/');
            var month6 = Mes6.Replace('-', '/');
            var db = DbConnection();

            var sql = @"
                        select  
                        (
                        select (sum(CAST(REPLACE(cantidad_1, ',', '') as DECIMAL)))
                        from public."+ configuration.GetValue<string>("BdProduccion:ventas_anual")!+ @" where TRIM(articulo) = '" + Articulo + @"' and fecha_emitida like '%/" + month1 + @"'
                        ) as month1 , 
                        (
                        select (sum(CAST(REPLACE(cantidad_1, ',', '') as DECIMAL)))
                        from public."+ configuration.GetValue<string>("BdProduccion:ventas_anual")!+ @" where TRIM(articulo) = '" + Articulo + @"' and fecha_emitida like '%/" + month2 + @"'
                        ) as month2,
                        (
                        select (sum(CAST(REPLACE(cantidad_1, ',', '') as DECIMAL))) 
                        from public."+ configuration.GetValue<string>("BdProduccion:ventas_anual")!+ @" where TRIM(articulo) = '" + Articulo + @"' and fecha_emitida like '%/" + month3 + @"'
                        ) as month3 ,
                        (
                        select (sum(CAST(REPLACE(cantidad_1, ',', '') as DECIMAL))) 
                        from public."+ configuration.GetValue<string>("BdProduccion:ventas_anual")!+ @" where TRIM(articulo) = '" + Articulo + @"' and fecha_emitida like '%/" + month4 + @"'
                        ) as month4 ,
                        (
                        select (sum(CAST(REPLACE(cantidad_1, ',', '') as DECIMAL))) 
                        from public."+ configuration.GetValue<string>("BdProduccion:ventas_anual")!+ @" where TRIM(articulo) = '" + Articulo + @"' and fecha_emitida like '%/" + month5 + @"'
                        ) as month5 ,
                        (
                        select (sum(CAST(REPLACE(cantidad_1, ',', '') as DECIMAL))) 
                        from public."+ configuration.GetValue<string>("BdProduccion:ventas_anual")!+ @" where TRIM(articulo) = '" + Articulo + @"' and fecha_emitida like '%/" + month6 + @"'
                        ) as month6 
                       ";
            var result = await db.QueryFirstOrDefaultAsync<TlMonthList>(sql, new { });
            return result!;
        }

        public async Task<IEnumerable<TlDetallelote>> ListarDetalleLoteImportacion(string Limit, string Offset, string Articulo, string Fecha_upload)
        {
            var db = DbConnection();

            var sql = @"
                        SELECT codigo, equivalente, descripcion, mhfech, importacion, tc, stock_actual, cantidad,
                        fob, costo_unit, lote, costo_lote, costo_unit_promedio, precio_venta_mn, precio_venta_mn_proy,
                        dolares, actual, proy, campo1, utilidad_promedio, venta_2024, venta_2023, venta_2022,
                        venta_2021, venta_2020, venta_2019, venta_2013, flete, mon  
                        FROM public."+ configuration.GetValue<string>("BdProduccion:analisis_lotes")!+ @"   
                        where codigo like upper('%" + Articulo + @"%') and fecha_upload = '" + Fecha_upload + @"' 
                        limit '" + Limit + "' offset '" + Offset + "' ;";

            return await db.QueryAsync<TlDetallelote>(sql, new { });
        }

        public async Task<IEnumerable<TlDetallelote>> ListarDetalleLoteImportacionAll(string Articulo, string Fecha_upload)
        {
            var db = DbConnection();

            var sql = @"
                        SELECT codigo, equivalente, descripcion, mhfech, importacion, tc, stock_actual, cantidad,
                        fob, costo_unit, lote, costo_lote, costo_unit_promedio, precio_venta_mn, precio_venta_mn_proy,
                        dolares, actual, proy, campo1, utilidad_promedio, venta_2024, venta_2023, venta_2022,
                        venta_2021, venta_2020, venta_2019, venta_2013, flete, mon  
                        FROM public."+ configuration.GetValue<string>("BdProduccion:analisis_lotes")!+ @"    
                        where codigo like upper('%" + Articulo + @"%') and fecha_upload = '" + Fecha_upload + @"'";

            return await db.QueryAsync<TlDetallelote>(sql, new { });
        }

        public async Task<TlDetLote> TotalDetalleLote(string Articulo, string Tc, string Desc, float Igv, string Fecha_upload)
        {
            var db = DbConnection();

            var sql = @"
                        select sum(cast(lote as integer)) as totallote,
                        ROUND(sum(cast(costo_lote as DECIMAL)),6) as totalcostolote,
                        ROUND((sum(cast(costo_lote as DECIMAL)))/(sum(cast(lote as integer))),6)  as totalcostounitario,
			ROUND(sum(((((CAST(precio_venta_mn as decimal)/CAST('" + Tc + @"' as decimal))*(1-(CAST('" + Desc + @"' as decimal)/100)))/NULLIF(((CAST(costo_unit as decimal))*" + Igv + @"),0))*(CAST(lote as integer)))),6) as totacampo,
			ROUND(((sum(((((CAST(precio_venta_mn as decimal)/CAST('" + Tc + @"' as decimal))*(1-(CAST('" + Desc + @"' as decimal)/100)))/NULLIF(((CAST(costo_unit as decimal))*" + Igv + @"),0))*(CAST(lote as integer)))))/
                        (sum(cast(lote as integer)))),6) as totalutilprom
                        from public."+ configuration.GetValue<string>("BdProduccion:analisis_lotes")!+ @"  
                        where codigo like upper('%" + Articulo + "%') and fecha_upload = '" + Fecha_upload + @"' ;";
            var result = await db.QueryFirstOrDefaultAsync<TlDetLote>(sql, new { });
            return result!;
        }

        public async Task<TlPedidos> TotalPedidoAnalisis(string Articulo)
        {
            var db = DbConnection();
            
            var sql = @"
                        select * from public."+ configuration.GetValue<string>("BdProduccion:pedidos_importacion")!+ @"
                        where codigo_interno like trim(upper('%" + Articulo + @"%'))
                       ";
            var result = await db.QueryFirstOrDefaultAsync<TlPedidos>(sql, new { });
            return result!;
        }
        
        /*------------------------------------------------------------------*/
        /*------------------------------------------------------------------*/
        /*------------------------------------------------------------------*/
        
        public async Task<IEnumerable<TlArticulo>> FilterCodigoInterno(string Limit, string Offset, string CodiInte, string Fecha_upload)
        {
            var db = DbConnection();

            var sql = @"
                        SELECT distinct codigo_interno
                        FROM public."+ configuration.GetValue<string>("BdProduccion:analisis_general")!+ @"  
                        where UPPER(codigo_interno) like UPPER('%" + CodiInte + @"%') and fecha_upload = '" + Fecha_upload + @"' 
                        order by codigo_interno asc
                        limit '" + Limit + "' offset '" + Offset + "' ;";

            return await db.QueryAsync<TlArticulo>(sql, new { });
        }
        
        /*------------------------------------------------------------------*/
        
        public async Task<IEnumerable<TlCodiEqui>> FilterCodigoEqui(string Limit, string Offset, string CodigoEqui, string Fecha_upload)
        {
            var db = DbConnection();

            var sql = @"
                        SELECT distinct codigo_interno, codigo_equivalente
                        FROM public."+ configuration.GetValue<string>("BdProduccion:analisis_general")!+ @"  
                        where UPPER(codigo_equivalente) like UPPER('%" + CodigoEqui + @"%') and fecha_upload = '" + Fecha_upload + @"' 
                        order by codigo_equivalente asc
                        limit '" + Limit + "' offset '" + Offset + "' ;";

            return await db.QueryAsync<TlCodiEqui>(sql, new { });
        }
        
        /*------------------------------------------------------------------*/
        
        public async Task<IEnumerable<TlCodiEqui>> FilterCodigoEquiFamilia(string Limit, string Offset, string Familia, string Fecha_upload)
        {
            var db = DbConnection();

            var sql = @"
                        SELECT distinct codigo_interno, codigo_equivalente
                        FROM public."+ configuration.GetValue<string>("BdProduccion:analisis_general")!+ @"  
                        where familia_codigo = '" + Familia + @"' and fecha_upload = '" + Fecha_upload + @"' 
                        order by codigo_equivalente asc
                        limit '" + Limit + "' offset '" + Offset + "' ;";

            return await db.QueryAsync<TlCodiEqui>(sql, new { });
        }
        
        /*------------------------------------------------------------------*/
        
        public async Task<IEnumerable<TlCodiEqui>> FilterCodigoEquiSubFamilia(string Limit, string Offset, string Familia, string SubFamilia, string Fecha_upload)
        {
            var db = DbConnection();

            var sql = @"
                        SELECT distinct codigo_interno, codigo_equivalente
                        FROM public."+ configuration.GetValue<string>("BdProduccion:analisis_general")!+ @"  
			            where familia_codigo = '" + Familia + "' and sub_familia_codigo = '" + SubFamilia + @"' and fecha_upload = '" + Fecha_upload + @"' 
                        order by codigo_equivalente asc
                        limit '" + Limit + "' offset '" + Offset + "' ;";

            return await db.QueryAsync<TlCodiEqui>(sql, new { });
        }
        
        /*------------------------------------------------------------------*/
        
        public async Task<IEnumerable<TlDescrip>> FilterDescripcion(string Limit, string Offset, string Descrip, string Fecha_upload)
        {
            var db = DbConnection();

            var sql = @"
                        SELECT distinct codigo_interno, codigo_equivalente, descripcion
                        FROM public."+ configuration.GetValue<string>("BdProduccion:analisis_general")!+ @"  
                        where UPPER(descripcion) like UPPER('%" + Descrip + @"%') and fecha_upload = '" + Fecha_upload + @"' 
                        order by descripcion asc
                        limit '" + Limit + "' offset '" + Offset + "' ;";

            return await db.QueryAsync<TlDescrip>(sql, new { });
        }
        
        public async Task<IEnumerable<TlDescrip>> FilterDescripcionFamilia(string Limit, string Offset, string Familia, string Fecha_upload)
        {
            var db = DbConnection();

            var sql = @"
                        SELECT distinct codigo_interno, codigo_equivalente, descripcion 
                        FROM public."+ configuration.GetValue<string>("BdProduccion:analisis_general")!+ @"  
			            where familia_codigo = '" + Familia + @"' and fecha_upload = '" + Fecha_upload + @"' 
                        order by descripcion asc 
                        limit '" + Limit + "' offset '" + Offset + "' ;";

            return await db.QueryAsync<TlDescrip>(sql, new { });
        }
        
        public async Task<IEnumerable<TlDescrip>> FilterDescripcionSubFamilia(string Limit, string Offset, string Familia, string SubFamilia, string Fecha_upload)
        {
            var db = DbConnection();

            var sql = @"
                        SELECT distinct codigo_interno, codigo_equivalente, descripcion 
                        FROM public."+ configuration.GetValue<string>("BdProduccion:analisis_general")!+ @"  
                        where familia_codigo = '" + Familia + "' and sub_familia_codigo = '" + SubFamilia + @"' and fecha_upload = '" + Fecha_upload + @"' 
                         order by descripcion asc 
                        limit '" + Limit + "' offset '" + Offset + "' ;";

            return await db.QueryAsync<TlDescrip>(sql, new { });
        }
        
        /*------------------------------------------------------------------*/
        
        public async Task<IEnumerable<TlProvefilter>> FilterProveedorfilter(string Limit, string Offset, string ProveCod, string Fecha_upload)
        {
            var db = DbConnection();

            var sql = @"
                        SELECT distinct proveedor_nombre, proveedor_codigo FROM public."+ configuration.GetValue<string>("BdProduccion:analisis_general")!+ @" 
                        where UPPER(proveedor_codigo) like UPPER('%" + ProveCod + @"%') and fecha_upload = '" + Fecha_upload + @"' 
                        order by proveedor_nombre asc
                        limit '" + Limit + "' offset '" + Offset + "' ;";

            return await db.QueryAsync<TlProvefilter>(sql, new { });
        }
        
        public async Task<IEnumerable<TlProvefilter>> FilterProveedor(string Limit, string Offset, string Fecha_upload)
        {
            var db = DbConnection();

            var sql = @"
                        SELECT distinct proveedor_nombre, proveedor_codigo FROM public."+ configuration.GetValue<string>("BdProduccion:analisis_general")!+ @" 
                        order by proveedor_nombre asc
                        where fecha_upload = '" + Fecha_upload + @"' 
                        limit '" + Limit + "' offset '" + Offset + "' ;";

            return await db.QueryAsync<TlProvefilter>(sql, new { });
        }
        
        public async Task<IEnumerable<TlProvefilter>> ListadorProveedorAll(string Fecha_upload)
        {
            var db = DbConnection();

            var sql = @"
                        SELECT distinct proveedor_nombre as proveedor_codigo, proveedor_codigo as proveedor_nombre 
                        FROM public."+ configuration.GetValue<string>("BdProduccion:analisis_general")!+ @" 
                        where proveedor_nombre IS NOT NULL and proveedor_codigo IS NOT NULL and fecha_upload = '" + Fecha_upload + @"' 
                        order by proveedor_nombre asc
                       ";
            return await db.QueryAsync<TlProvefilter>(sql, new { });
        }
        
        public async Task<IEnumerable<TlProvefilter>> FilterProveedorFamilia(string Limit, string Offset, string Familia, string Fecha_upload)
        {
            var db = DbConnection();

            var sql = @"
                        SELECT distinct proveedor_nombre, proveedor_codigo FROM public."+ configuration.GetValue<string>("BdProduccion:analisis_general")!+ @"  
			            where familia_codigo = '" + Familia + @"' and fecha_upload = '" + Fecha_upload + @"' 
                        order by proveedor_nombre asc 
                        limit '" + Limit + "' offset '" + Offset + "' ;";

            return await db.QueryAsync<TlProvefilter>(sql, new { });
        }
        
        public async Task<IEnumerable<TlProvefilter>> FilterProveedorSubFamilia(string Limit, string Offset, string Familia, string SubFamilia, string Fecha_upload)
        {
            var db = DbConnection();

            var sql = @"
                        SELECT distinct proveedor_nombre, proveedor_codigo FROM public."+ configuration.GetValue<string>("BdProduccion:analisis_general")!+ @" 
			            where familia_codigo = '" + Familia + "' and sub_familia_codigo = '" + SubFamilia + @"' and fecha_upload = '" + Fecha_upload + @"' 
                         order by proveedor_nombre asc
                        limit '" + Limit + "' offset '" + Offset + "' ;";

            return await db.QueryAsync<TlProvefilter>(sql, new { });
        }
        
        /*----------------------------------------------------------------------------------------------------------*/

        public async Task<TdImportacion> DetalleImportacion(string Articulo)
        {
            var db = DbConnection();
            //"+ configuration.GetValue<string>("BdProduccion:analisis_importacion")!+ @" 
            var sql = @"
                        select codigo, fechaacumulado, nombrepedido, cantidadpedido, nombreviajando, cantidadviajando,
                        Cast(SUM(cast(cantidadpedido as INT)+ cast(cantidadviajando as INT)) as varchar) as sumaviajando 
                        from public."+ configuration.GetValue<string>("BdProduccion:analisis_importacion")!+ @"  where codigo = '" + Articulo + @"'
                        group by codigo, fechaacumulado, nombrepedido, cantidadpedido, nombreviajando, cantidadviajando
                       ";
            var result = await db.QueryFirstOrDefaultAsync<TdImportacion>(sql, new { });
            return result!;
        }
        
        /*
        select codigo, fechaacumulado, nombrepedido, cantidadpedido, nombreviajando, cantidadviajando,
        Cast(SUM(cast(cantidadpedido as INT)+ cast(cantidadviajando as INT)) as varchar) as sumaviajando 
        from public."+ configuration.GetValue<string>("BdProduccion:analisis_importacion")!+ @"  where codigo = 'CHN0017518'
        group by codigo, fechaacumulado, nombrepedido, cantidadpedido, nombreviajando, cantidadviajando  
        */

    }
}
