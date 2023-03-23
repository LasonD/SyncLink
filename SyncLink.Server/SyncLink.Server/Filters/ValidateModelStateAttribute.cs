using Microsoft.AspNetCore.Mvc.Filters;
using SyncLink.Server.Exceptions;

namespace SyncLink.Server.Filters;

public class ValidateModelStateAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid)
        {
            return;
        }

        throw new ModelValidationException(context.ModelState);
    }
}