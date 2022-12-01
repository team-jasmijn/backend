﻿using Data.Helpers;
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
        if (context.HttpContext.User.Identity?.IsAuthenticated is true && !context.HttpContext.User.IsStudent())
            return;

        context.Result = new BadRequestObjectResult(new
        {
            authorization = "User is not authenticated or not a student"
        });
    }
}
