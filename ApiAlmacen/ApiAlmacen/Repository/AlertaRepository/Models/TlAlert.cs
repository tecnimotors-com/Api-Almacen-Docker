namespace ApiAlmacen.Repository.AlertaRepository.Models
{
    public class TlAlert
    {
       public string? Codigo_interno{ get;set;}
        public string? Codigo_equivalente { get;set;}
        public string? Descripcion { get;set;}
        public string? Total_stock { get;set;}
        public string? Prom_venta_2024 { get;set;} 
        public float AlertaInventario{ get;set;}
    }
}
