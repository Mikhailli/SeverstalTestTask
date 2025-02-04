using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using OrderService.ApiRequestModels;
using OrderService.Services.Interfaces;

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
    public IActionResult Delete([Required] int id)
    {
        _orderService.DeleteOrder(id);

        return NoContent();
    }

    [HttpPut]
    [Route("{id}")]
    public IActionResult ChangeOrderItems([Required] int id, [FromBody] ICollection<OrderItemParameters> parameters)
    {
        _orderService.ChangeOrderItems(id, parameters);

        return NoContent();
    }

    [HttpPut]
    [Route("{id}/{status}")]
    public IActionResult ChangeStatus([Required] int id, [Required] string status)
    {
        _orderService.ChangeStatus(id, status);

        return NoContent();
    }
}
