using Data.Helpers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace EasyIntern_Backend.Attributes.Filters;

public class IsCompanyFilter : ActionFilterAttribute
{
    public IsCompanyFilter()
    {
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.HttpContext.User.IsStudent())
            return;

        context.Result = new BadRequestResult();
    }
}
