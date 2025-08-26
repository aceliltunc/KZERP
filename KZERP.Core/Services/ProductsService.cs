using KZERP.Core.Entities;
using KZERP.Core.Interfaces;

namespace KZERP.Core.Services;

public class ProductsService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductsService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _productRepository.GetAllAsync();
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid ID");

        return await _productRepository.GetByIdAsync(id);
    }

    public async Task AddProductAsync(Product product)
    {
        if (string.IsNullOrEmpty(product.Name))
            throw new ArgumentException("Product name required");

        await _productRepository.AddAsync(product);
    }

    public async Task UpdateProductAsync(Product product)
    {
        await _productRepository.UpdateAsync(product);
    }

    public async Task DeleteProductAsync(int id)
    {
        await _productRepository.DeleteAsync(id);
    }
}
