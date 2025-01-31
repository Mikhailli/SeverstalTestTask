using Common.DataAccess.Models;

namespace ProductService.Services.Interfaces;

public interface IProductService
{
    Task<Product> GetAsync(int id);

    Product[] GetAll();
}
