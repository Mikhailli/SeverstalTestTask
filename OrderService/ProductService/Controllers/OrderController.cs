using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ProductService.Services.Interfaces;

namespace ProductService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController(IOrderService orderService) : Controller
{
    private readonly IOrderService _orderService = orderService;

    [HttpGet]
    public IActionResult GetAll()
    {
        var orders = _orderService.GetAll();

        return Json(orders);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Get([Required] int id)
    {
        var order = await _orderService.GetAsync(id);

        return Ok(order);
    }
}
