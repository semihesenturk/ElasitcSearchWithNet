using ES.ElasticSearch.Web.Services.ECommerce;
using ES.ElasticSearch.Web.ViewModel.ECommerce;
using Microsoft.AspNetCore.Mvc;

namespace ES.ElasticSearch.Web.Controllers;

public class ECommerceController(ECommerceService eCommerceService) : Controller
{
    private readonly ECommerceService eCommerceService = eCommerceService;

    [HttpGet]
    public async Task<IActionResult> Search([FromQuery] SearchPageViewModel searchPageView)
    {
        var (eCommerceList, totalCount, pageLinkCount)  = await eCommerceService
            .SearchAsync(searchPageView.SearchViewModel, searchPageView.Page, searchPageView.PageSize);

        searchPageView.List = eCommerceList;
        searchPageView.TotalCount = totalCount;
        searchPageView.PageLinkCount = pageLinkCount;
        
        
        return View(searchPageView);
    }
}