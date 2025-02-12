using ES.ElasticSearch.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ES.ElasticSearch.API.Controllers;

[Route("api/[controller]/[action]")]
public class ECommerceController(ECommerceService eCommerceService) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> TermQuery(string customerFirstName)
    {
        return CreateActionResult(await eCommerceService.GetWithCustomerFirstNameTerm(customerFirstName));
    }
}