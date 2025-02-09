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
}