using ES.ElasticSearch.Web.Services;
using ES.ElasticSearch.Web.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ES.ElasticSearch.Web.Controllers;

public class BlogController(BlogService blogService) : Controller
{
    [HttpGet]
    public IActionResult Save()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Save(BlogCreateViewModel model)
    {
        var result = await blogService.SaveAsync(model);

        if (!result)
        {
            TempData["result"] = "Failed to save blog";
            return RedirectToAction(nameof(Save));
        }
        
        TempData["result"] = "Saved blog";
        return RedirectToAction(nameof(Save));
    }
}