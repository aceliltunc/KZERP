
using KZERP.Core.Entities.Products;
using KZERP.Core.Interfaces.IProductRepository;
using KZERP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace KZERP.Infrastructure.Repository.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly KZERPDbContext _context;

        public ProductRepository(KZERPDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }


        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                throw new NullReferenceException($"Product with ID {id} not found.");
            }

            return product;
        }

        public async Task AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await GetProductByIdAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}