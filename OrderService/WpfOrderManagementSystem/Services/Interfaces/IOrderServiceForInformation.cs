using WpfOrderManagementSystem.Models;

namespace WpfOrderManagementSystem.Services.Interfaces;

internal interface IOrderServiceForInformation
{
    Task<Order> GetAsync(int id);

    Task<Order[]> GetAllAsync();
}
