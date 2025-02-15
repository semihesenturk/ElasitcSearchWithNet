using ES.ElasticSearch.Web.Services;
using ES.ElasticSearch.Web.Services.Blog;
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

    [HttpGet]
    public async Task<IActionResult> Search()
    {
        return View(await blogService.SearchAsync(string.Empty));
    }

    [HttpPost]
    public async Task<IActionResult> Search(string searchText)
    {
        ViewBag.SearchText = searchText;
        return View(await blogService.SearchAsync(searchText));
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