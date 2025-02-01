using WpfOrderManagementSystem.Models;

namespace WpfOrderManagementSystem.Services.Interfaces;

internal interface IProductServiceForInformation
{
    Task<Product> GetAsync(int id);

    Task<Product[]> GetAllAsync();
}
