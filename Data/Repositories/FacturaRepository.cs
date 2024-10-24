using Microsoft.EntityFrameworkCore;
using ONDACTest.Data.Models;

namespace ONDACTest.Data.Repositories
{
    public class FacturaRepository : IFacturaRepository
    {

        private readonly ApplicationDbContext dbContext;

        public FacturaRepository(ApplicationDbContext context)
        {
            dbContext = context;
        }

        public async Task AddAsync(Factura factura)
        {
            await dbContext.FacturaCabecera.AddAsync(factura.Cabecera);

            await dbContext.SaveChangesAsync();

            foreach (FacturaDetalle detalle in factura.Detalles)
            {
                detalle.IdFactura = factura.Cabecera.IdFactura;
                await dbContext.FacturaDetalle.AddAsync(detalle);
            }

            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> CanBeDeletedAsync(int id)
        {
            FacturaCabecera? factura = await dbContext.FacturaCabecera.FindAsync(id);
            
            if (factura != null && factura.IdEstadoFactura == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task DeleteAsync(int id)
        {
            FacturaCabecera? facturaCabecera = await dbContext.FacturaCabecera
              .Include(f => f.FacturaDetalles)
              .FirstOrDefaultAsync(f => f.IdFactura == id);

            if (facturaCabecera != null)
            {
                if (facturaCabecera.FacturaDetalles.Any())
                {
                    dbContext.FacturaDetalle.RemoveRange(facturaCabecera.FacturaDetalles);
                }
                dbContext.FacturaCabecera.Remove(facturaCabecera);
                await dbContext.SaveChangesAsync();
            } 
        }

        public async Task<IEnumerable<Factura>> GetAllAsync()
        {
            var facturasCabecera = await dbContext.FacturaCabecera
                .Include(f => f.FacturaDetalles)
                .Include(f => f.Proveedor)
                .ToListAsync();
            
            var facturas = facturasCabecera.Select(f => new Factura 
            { 
                Cabecera = f, 
                Detalles = f.FacturaDetalles,
                NombreProveedor = f.Proveedor?.NombreProveedor,
                IdProveedor = f.IdProveedor
                
            });
            
            return facturas;    
        }

        public async Task<IEnumerable<Factura>> GetAllByProveedorAsync(int id)
        {
            var facturasCabecera = await dbContext.FacturaCabecera
                .Include(f => f.FacturaDetalles)
                .Where(p => p.Proveedor!.IdProveedor == id)
                .ToListAsync();

            var facturas = facturasCabecera.Select(f => new Factura
            {
                Cabecera = f,
                Detalles = f.FacturaDetalles,
                NombreProveedor = f.Proveedor?.NombreProveedor,
                IdProveedor = f.IdProveedor
            });

            return facturas;
        }

        public async Task<Factura?> GetByIdAsync(int id)
        {
            Factura factura;

            var facturaCabecera = await dbContext.FacturaCabecera
                .Include(f => f.FacturaDetalles)
                .Include(f => f.Proveedor)
                .FirstOrDefaultAsync(f => f.IdFactura == id);

            if(facturaCabecera != null)
            {
                factura = new()
                {
                    Cabecera = facturaCabecera,
                    Detalles = facturaCabecera!.FacturaDetalles,
                    NombreProveedor = facturaCabecera.Proveedor!.NombreProveedor,
                    IdProveedor = facturaCabecera.IdProveedor
                };
                return factura;
            }
            else
            {
                return null;
            }

            
        }

        public async Task<IEnumerable<Factura>> SearchByProveedorAsync(string searchTerm)
        {

            var facturasCabecera = await dbContext.FacturaCabecera
                .Include(f => f.FacturaDetalles)
                .Include(f => f.Proveedor)
                .Where(p => p.Proveedor!.NombreProveedor.Contains(searchTerm))
                .ToListAsync();

            var facturas = facturasCabecera.Select(f => new Factura
            {
                Cabecera = f,
                Detalles = f.FacturaDetalles,
                NombreProveedor = f.Proveedor?.NombreProveedor,
                IdProveedor = f.IdProveedor
            });

            return facturas;
        }

        public async Task UpdateAsync(Factura factura)
        {
            var detallesExistentes = await dbContext.FacturaDetalle
                .Where(d => d.IdFactura == factura.Cabecera.IdFactura)
                .ToListAsync();

            
            dbContext.Entry(factura.Cabecera).State = EntityState.Modified;

            foreach (var detalle in factura.Detalles)
            {
                if (detalle.IdFacturaDetalle != 0)
                {
                    dbContext.Entry(detalle).State = EntityState.Modified;
                }
                else
                {
                    detalle.IdFactura = factura.Cabecera.IdFactura;
                    await dbContext.FacturaDetalle.AddAsync(detalle);
                }
            }

            var detallesAEliminar = detallesExistentes
                .Where(d => !factura.Detalles
                    .Any(nd => nd.IdFacturaDetalle == d.IdFacturaDetalle))
                .ToList();

            dbContext.FacturaDetalle.RemoveRange(detallesAEliminar);

            await dbContext.SaveChangesAsync();
        }
    }
}
