using be.Data;
using be.DTOs;
using be.Models;
using be.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace be.Services.Implements
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(string id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.id == id);
        }
    }
}