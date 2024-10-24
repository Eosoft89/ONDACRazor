namespace ONDACTest.Data.Models
{
    public class Factura
    {
        public FacturaCabecera Cabecera { get; set; }
        public List<FacturaDetalle> Detalles { get; set; }
        public string? NombreProveedor { get; set; }
        public int IdProveedor { get; set;}
        public Factura()
        {
            Cabecera = new()
            {
                FechaFactura = DateTime.Now,
                IdEstadoFactura = 1
            };
            
            Detalles = new();
        }
    }
}
