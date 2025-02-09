using ES.ElasticSearch.API.Dtos;
using ES.ElasticSearch.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ES.ElasticSearch.API.Controllers;

public class ProductsController(ProductService productService) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Save(ProductCreateDto request)
    {
        return CreateActionResult(await productService.SaveAsync(request));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return CreateActionResult(await productService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        return CreateActionResult(await productService.GetByIdAsync(id));
    }
}