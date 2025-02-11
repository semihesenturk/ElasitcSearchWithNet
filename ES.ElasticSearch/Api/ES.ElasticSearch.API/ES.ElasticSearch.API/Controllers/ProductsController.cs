using ES.ElasticSearch.API.Dtos;
using ES.ElasticSearch.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ES.ElasticSearch.API.Controllers;

public class ProductsController(ProductService productService) : BaseController
{
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
    
    [HttpPost]
    public async Task<IActionResult> Save(ProductCreateDto request)
    {
        return CreateActionResult(await productService.SaveAsync(request));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProductUpdateDto request)
    {
        return CreateActionResult(await productService.UpdateAsync(request));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        return CreateActionResult(await productService.DeleteAsync(id));
    }
}