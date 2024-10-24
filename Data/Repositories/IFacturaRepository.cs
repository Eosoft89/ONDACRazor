using ONDACTest.Data.Models;

namespace ONDACTest.Data.Repositories
{
    public interface IFacturaRepository
    {
        Task<IEnumerable<Factura>> GetAllAsync();
        Task<Factura?> GetByIdAsync(int id);
        Task AddAsync(Factura factura);
        Task UpdateAsync(Factura factura);
        Task DeleteAsync(int id);
        Task<bool> CanBeDeletedAsync(int id);
        Task<IEnumerable<Factura>> SearchByProveedorAsync(string searchTerm);
        Task<IEnumerable<Factura>> GetAllByProveedorAsync(int id);

    }
}
