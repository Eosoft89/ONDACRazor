using Microsoft.EntityFrameworkCore;
using ONDACTest.Data.Models;
using System.Reflection.Metadata.Ecma335;

namespace ONDACTest.Data.Repositories
{
    public class ProveedorRepository : IProveedorRepository
    {
        private readonly ApplicationDbContext dbContext;

        public ProveedorRepository(ApplicationDbContext context)
        {
            dbContext = context;
        }
        public async Task AddAsync(Proveedor proveedor)
        {
            dbContext.Add(proveedor);
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> CanBeDeletedAsync(int id)
        {
            bool result = await dbContext.FacturaCabecera.AnyAsync(f => f.IdProveedor == id);
            return !result;
        }

        public async Task DeleteAsync(int id)
        {
            Proveedor? proveedor = await dbContext.Proveedor.FindAsync(id);
            if (proveedor != null)
            {
                dbContext.Proveedor.Remove(proveedor);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Proveedor>> GetAllAsync()
        {
            return await dbContext.Proveedor.ToListAsync();
        }

        public async Task<Proveedor?> GetByIdAsync(int id)
        {
            return await dbContext.Proveedor
                .Include(f => f.Facturas)
                .FirstOrDefaultAsync(p => p.IdProveedor == id);
                
        }

        public async Task<IEnumerable<Proveedor>> SearchByNameAsync(string searchTerm)
        {
            return await dbContext.Proveedor
                .Where(p => p.NombreProveedor.Contains(searchTerm))
                .ToListAsync();
        }

        public async Task<IEnumerable<Proveedor>> SearchByRutAsync(string searchTerm)
        {
            return await dbContext.Proveedor
                .Where(p => p.RutProveedor != null && p.RutProveedor.Contains(searchTerm))
                .ToListAsync();
        }

        public async Task UpdateAsync(Proveedor proveedor)
        {
            dbContext.Proveedor.Update(proveedor);
            await dbContext.SaveChangesAsync(); 
        }
    }
}
