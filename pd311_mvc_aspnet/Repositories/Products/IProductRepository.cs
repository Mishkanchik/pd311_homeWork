using pd311_mvc_aspnet.Models;

namespace pd311_mvc_aspnet.Repositories.Products
{
    public interface ICategoryRepository
    {
        Task<bool> CreateAsync(Product model);
        Task<bool> UpdateAsync(Product model);
        Task<bool> DeleteAsync(string id);
        Task<List<Product>> GetAllAsync();
        Task<Product?> FindByIdAsync(string id);
    }
}
