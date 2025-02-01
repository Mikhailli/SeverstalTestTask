using WpfOrderManagementSystem.Models;
using WpfOrderManagementSystem.Models.ApiRequestModels;

namespace WpfOrderManagementSystem.Services.Interfaces;

internal interface IServiceForOrderManagement
{
    Task<Order> AddOrderAsync(OrderParameters parameters);

    Task DeleteOrderAsync(int id);

    Task ChangeStatusAsync(int id, string status);

    Task AddProductToOrderAsync(int id, int productId);

    Task RemoveProductFromOrderAsync(int id, int productId);
}
