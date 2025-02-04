using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using OrderService.ApiRequestModels;
using OrderService.Services.Interfaces;

namespace OrderService.Controllers;

/// <summary>
/// Контроллер для управления заказами
/// </summary>
/// <param name="orderService">Сервис для управления заказами</param>
[ApiController]
[Route("api/[controller]")]
public class OrderController(IOrderService orderService) : Controller
{
    private readonly IOrderService _orderService = orderService;

    /// <summary>
    /// Добавление нового заказа
    /// </summary>
    /// <param name="parameters">Параметры заказа</param>
    [HttpPost]
    public IActionResult AddOrder([FromBody] OrderParameters parameters)
    {
        var order = _orderService.AddOrder(parameters.OrderDate, parameters.StorageUntil, parameters.Status, parameters.CustomerName, parameters.PhoneNumber);

        return Json(order);
    }

    /// <summary>
    /// Удаление заказа
    /// </summary>
    /// <param name="id">Идентификатор заказа для удаления</param>
    [HttpDelete("{id}")]
    public IActionResult Delete([Required] int id)
    {
        _orderService.DeleteOrder(id);

        return NoContent();
    }

    /// <summary>
    /// Изменение товаров в заказе
    /// </summary>
    /// <param name="id">Идентификатор товара</param>
    /// <param name="parameters">Параметры для товаров в заказе</param>
    [HttpPut]
    [Route("{id}")]
    public IActionResult ChangeOrderItems([Required] int id, [FromBody] ICollection<OrderItemParameters> parameters)
    {
        _orderService.ChangeOrderItems(id, parameters);

        return NoContent();
    }

    /// <summary>
    /// Изменение статуса заказа
    /// </summary>
    /// <param name="id">Идентификатор заказа</param>
    /// <param name="status">Новый статус</param>
    [HttpPut]
    [Route("{id}/{status}")]
    public IActionResult ChangeStatus([Required] int id, [Required] string status)
    {
        _orderService.ChangeStatus(id, status);

        return NoContent();
    }
}
