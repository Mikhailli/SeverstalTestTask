using Common.DataAccess.Interfaces;
using Common.DataAccess.Models;
using ProductService.Services.Interfaces;

namespace ProductService.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product> GetAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product is null)
        {
            throw new Exception($"Товар с идентификатором {id} не найден");
        }

        return product;
    }

    public Product[] GetAll()
    {
        return _productRepository.GetAll().ToArray();
    }
}
