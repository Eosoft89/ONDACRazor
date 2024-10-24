using ONDACTest.Data.Models;

namespace ONDACTest.Data.Repositories
{
    public interface IProveedorRepository
    {
        Task<IEnumerable<Proveedor>> GetAllAsync();
        Task<Proveedor?> GetByIdAsync(int id);
        Task AddAsync(Proveedor proveedor);
        Task UpdateAsync(Proveedor proveedor);
        Task DeleteAsync(int id);
        Task<bool> CanBeDeletedAsync(int id);
        Task<IEnumerable<Proveedor>> SearchByNameAsync(string searchTerm);
        Task<IEnumerable<Proveedor>> SearchByRutAsync(string searchTerm);
    }
}
