using System.Net;
using ES.ElasticSearch.Web.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ES.ElasticSearch.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController : Controller
{
    [NonAction]
    public IActionResult CreateActionResult<T>(ResponseDto<T> response)
    {
        if (response.Status == HttpStatusCode.NoContent)
            return new ObjectResult(null) { StatusCode = response.Status.GetHashCode() };
        
        return new ObjectResult(response) { StatusCode = response.Status.GetHashCode() };
    }
}