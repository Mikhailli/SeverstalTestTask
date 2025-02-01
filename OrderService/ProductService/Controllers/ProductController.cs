using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ProductService.Services.Interfaces;

namespace ProductService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(IProductService productService) : Controller
{
    private readonly IProductService _productService = productService;

    [HttpGet]
    public IActionResult GetAll()
    {
        var products = _productService.GetAll();

        return Json(products);
    }

    /// <summary>
    /// Получает информацию о товаре по его идентификатору
    /// </summary>
    /// <param name="id">Идентификатор товара</param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Get([Required] int id)
    {
        var product = await _productService.GetAsync(id);

        return Ok(product);
    }
}
