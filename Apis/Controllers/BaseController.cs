using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Apis.Controllers;

[ApiController]
[Route("[controller]")]
[ProducesResponseType(typeof(ValidationExceptionModel), 400)]
[ProducesResponseType(typeof(DefaultExceptionModel), 500)]
public class BaseController : ControllerBase
{
    protected IActionResult GetViewResult<T>(
        string viewName,
        T model)
    {
        return new ViewResult()
        {
            ViewName = viewName,
            ViewData = new ViewDataDictionary(MetadataProvider , ModelState)
            {
                Model = model
            }
        };
    }
 
}
