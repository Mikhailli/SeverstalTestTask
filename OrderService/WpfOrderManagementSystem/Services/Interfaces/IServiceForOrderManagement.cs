using WpfOrderManagementSystem.Models;
using WpfOrderManagementSystem.Models.ApiRequestModels;

namespace WpfOrderManagementSystem.Services.Interfaces;

internal interface IServiceForOrderManagement
{
    Task<Order> AddOrderAsync(OrderParameters parameters);

    Task DeleteOrderAsync(int id);

    Task ChangeStatusAsync(int id, string status);

    Task ChangeOrderItemsAsync(int id, ICollection<(int productId, int quantity)> orderItems);
}
