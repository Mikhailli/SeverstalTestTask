using Common.DataAccess.Models;

namespace ProductService.Services.Interfaces;

public interface IOrderService
{
    Task<Order> GetAsync(int id);

    Order[] GetAll();
}