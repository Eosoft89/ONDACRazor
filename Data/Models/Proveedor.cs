using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ONDACTest.Data.Models
{
    [Table("tblProveedor")]
    public class Proveedor
    {
        [Key, Column("IdProveedor")]
        public int IdProveedor { get; set; }

        [Required, Column("NombreProveedor", TypeName = "varchar(500)")]
        public string NombreProveedor { get; set; } = string.Empty;

        [Column("ComunaProveedor", TypeName = "varchar(100)")]
        public string? ComunaProveedor { get; set; }

        [Column("CiudadProveedor", TypeName = "varchar(100)")]
        public string? CiudadProveedor { get; set; }

        [Column("FonoProveedor", TypeName = "varchar(50)")]
        public string? FonoProveedor { get; set; }

        [Column("RutProveedor", TypeName = "varchar(50)")]
        public string? RutProveedor { get; set; }

        [Column("GiroProveedor", TypeName = "varchar(200)")]
        public string? GiroProveedor { get; set; }

        public virtual ICollection<FacturaCabecera> Facturas { get; set; } = new List<FacturaCabecera>();
    }
}
