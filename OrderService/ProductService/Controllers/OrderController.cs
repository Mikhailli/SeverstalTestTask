using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ProductService.Services.Interfaces;

namespace ProductService.Controllers;

/// <summary>
/// Контроллер для получения информации о заказах
/// </summary>
/// <param name="orderService">Сервис для получения информации о заказах</param>
[ApiController]
[Route("api/[controller]")]
public class OrderController(IOrderService orderService) : Controller
{
    private readonly IOrderService _orderService = orderService;

    /// <summary>
    /// Получение всех заказов
    /// </summary>
    [HttpGet]
    public IActionResult GetAll()
    {
        var orders = _orderService.GetAll();

        return Json(orders);
    }

    /// <summary>
    /// Получение заказа
    /// </summary>
    /// <param name="id">Идентификатор заказа</param>
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Get([Required] int id)
    {
        var order = await _orderService.GetAsync(id);

        return Ok(order);
    }
}
