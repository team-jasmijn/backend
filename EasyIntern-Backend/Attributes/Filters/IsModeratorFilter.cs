using Data.Helpers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace EasyIntern_Backend.Attributes.Filters;

public class IsModeratorFilter : ActionFilterAttribute
{
  public IsModeratorFilter()
  {
  }

  public override void OnActionExecuting(ActionExecutingContext context)
  {
    if (context.HttpContext.User.Identity?.IsAuthenticated is true && context.HttpContext.User.IsModerator())
      return;

    context.Result = new BadRequestObjectResult(new
    {
      authorization = "User is not authenticated or not a moderator"
    });
  }
}
