using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using OrderService.ApiRequestModels;
using ProductService.Services.Interfaces;

namespace OrderService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController(IOrderService orderService) : Controller
{
    private readonly IOrderService _orderService = orderService;

    [HttpPost]
    public IActionResult AddOrder([FromBody] OrderParameters parameters)
    {
        var order = _orderService.AddOrder(parameters.OrderDate, parameters.StorageUntil, parameters.Status, parameters.CustomerName, parameters.PhoneNumber);

        return Json(order);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteOrder([Required] int id)
    {
        _orderService.DeleteOrder(id);

        return NoContent();
    }

    [HttpPut]
    [Route("addProduct/{id}/{productId}")]
    public IActionResult AddProductToOrder([Required] int id, [Required] int productId)
    {
        _orderService.AddProductToOrder(id, productId);

        return NoContent();
    }

    [HttpPut]
    [Route("removeProduct/{id}/{productId}")]
    public IActionResult RemoveProductFromOrder([Required] int id, [Required] int productId)
    {
        _orderService.RemoveProductFromOrder(id, productId);

        return NoContent();
    }

    [HttpPut]
    [Route("changeStatus/{id}/{status}")]
    public IActionResult ChangeStatus([Required] int id, [Required] string status)
    {
        _orderService.ChangeStatus(id, status);

        return NoContent();
    }
}
