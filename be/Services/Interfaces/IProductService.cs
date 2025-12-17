using be.DTOs;
using be.Models;

namespace be.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(string id);
    }
}