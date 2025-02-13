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

    [HttpPost]
    public async Task<IActionResult> TermsQuery(List<string> customerFirstNameList)
    {
        return CreateActionResult(await eCommerceService.TermsQuery(customerFirstNameList));
    }

    [HttpGet]
    public async Task<IActionResult> PrefixQuery(string customerFullName)
    {
        return CreateActionResult(await eCommerceService.PrefixQuery(customerFullName));
    }

    [HttpGet]
    public async Task<IActionResult> RangeQuery(double fromPrice, double toPrice)
    {
        return CreateActionResult(await eCommerceService.RangeQuery(fromPrice, toPrice));
    }

    [HttpGet]
    public async Task<IActionResult> MatchAllQuery()
    {
        return CreateActionResult(await eCommerceService.MatchAllQuery());
    }

    [HttpGet]
    public async Task<IActionResult> PaginationQuery(int page, int pageSize)
    {
        return CreateActionResult(await eCommerceService.PaginationQueryAsync(page, pageSize));
    }

    [HttpGet]
    public async Task<IActionResult> WildCardQuery(string customerFullName)
    {
        return CreateActionResult(await eCommerceService.WildCardQueryAsync(customerFullName));
    }

    [HttpGet]
    public async Task<IActionResult> FuzzyQuery(string customerName)
    {
        return CreateActionResult(await eCommerceService.FuzzyQueryAsync(customerName));
    }

    [HttpGet]
    public async Task<IActionResult> MatchQueryFullText(string categoryName)
    {
        return CreateActionResult(await eCommerceService.MatchQueryFullTextAsync(categoryName));
    }

    [HttpGet]
    public async Task<IActionResult> MatchBoolPrefixFullTextQuery(string customerFullName)
    {
        return CreateActionResult(await eCommerceService.MatchBooPrefixFullTextQueryAsync(customerFullName));
    }
}