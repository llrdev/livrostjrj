using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Webapi.Core.Response;
using Webapi.Extensions;

namespace Webapi.Filters
{
    public class ModelStateValidatorFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(error: new Response<string>(context.ModelState.GetFailures()));
            }
        }
    }
}
