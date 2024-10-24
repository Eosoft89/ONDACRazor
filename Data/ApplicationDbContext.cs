using Microsoft.EntityFrameworkCore;
using ONDACTest.Data.Models;

namespace ONDACTest.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> contextOptions) : base(contextOptions)
        {

        }

        public DbSet<FacturaCabecera> FacturaCabecera { get; set; }
        public DbSet<FacturaDetalle> FacturaDetalle { get; set; }
        public DbSet<Proveedor> Proveedor { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FacturaCabecera>()
                .HasMany(f => f.FacturaDetalles)
                .WithOne(fd => fd.FacturaCabecera)
                .HasForeignKey(fd => fd.IdFactura);

            modelBuilder.Entity<Proveedor>()
                .HasMany(p => p.Facturas)
                .WithOne(f => f.Proveedor)
                .HasForeignKey(f => f.IdProveedor);

        }

    }
}
