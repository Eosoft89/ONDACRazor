using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ONDACTest.Data.Models
{
    [Table("tblFacturaCabecera")]
    public class FacturaCabecera
    {
        [Key, Column("IdFactura")]
        public int IdFactura { get; set; }

        [Required, Column("FechaFactura")]
        public DateTime FechaFactura { get; set; }

        [Required, Column("IdEstadoFactura")]
        public int IdEstadoFactura { get; set; }

        [Required, Column("GlosaFactura", TypeName = "varchar(1000)")]
        public string? GlosaFactura { get; set; }

        [Required, Column("IdProveedor")]
        public int IdProveedor { get; set; }

        [ForeignKey("IdProveedor")]
        public Proveedor? Proveedor { get; set; }

        [Required, Column("ExentaIVA", TypeName = "bit")]
        public bool ExentaIVA { get; set; }

        [NotMapped]
        public string NombreEstadoFactura
        {
            get
            {
                return IdEstadoFactura switch
                {
                    1 => "Borrador",
                    2 => "Enviada",
                    3 => "Aceptada",
                    4 => "Aceptada con observaciones",
                    5 => "Rechazada",
                    _ => "Desconocido"
                };
            }
        }

        public virtual List<FacturaDetalle> FacturaDetalles { get; set; } = new List<FacturaDetalle>();
    }

}
