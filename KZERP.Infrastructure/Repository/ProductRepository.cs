
namespace KZERP.Infrastructure.Repository;

using KZERP.Core.Entities;
using KZERP.Core.Interfaces;
using KZERP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class ProductRepository : IProductRepository
{
    private readonly KZERPDbContext _context;

    public ProductRepository(KZERPDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    
    public async Task<Product> GetByIdAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            throw new NullReferenceException($"Product with ID {id} not found.");
        }

        return product;
    }

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var product = await GetByIdAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}