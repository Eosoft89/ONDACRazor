using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ONDACTest.Data.Models
{
    [Table("tblFacturaDetalle")]
    public class FacturaDetalle
    {
        public FacturaDetalle()
        {
            NroItem = 1;
        }

        [Key, Column("IdFacturaDetalle")]
        public int IdFacturaDetalle { get; set; }

        [Required, Column("IdFactura")]
        public int IdFactura { get; set; }

        [ForeignKey("IdFactura")]
        public FacturaCabecera? FacturaCabecera { get; set; }

        [Required, Column("NroItem")]
        public int NroItem { get; set; }

        [Required, Column("CodigoItem", TypeName = "varchar(50)")]
        public string? CodigoItem { get; set; }

        [Column("NombreItem", TypeName = "varchar(500)")]
        public string? NombreItem { get; set; }

        [Column("UnidadItem", TypeName = "varchar(10)")]
        public string? UnidadItem { get; set; }

        [Column("CantidadItem", TypeName = "decimal(37, 6)")]
        public decimal? CantidadItem { get; set; }

        [Column("PrecioUnitarioItem", TypeName = "decimal(37,6)")]
        public decimal? PrecioUnitarioItem { get; set; }

        [Column("SubTotalItem", TypeName = "decimal(37,6)")]
        public decimal? SubTotalItem { get; set; }
    }
}
