using Microsoft.AspNetCore.Mvc;

namespace EasyIntern_Backend.Controllers
{
    [Route("error")]
    public class ErrorController : Controller
    {
        [Route("")]
        public IActionResult Index(string status = null)
        {
            switch (status)
            {
                case "404":
                    ModelState.AddModelError("NotFound", "Page was not found");
                    break;
                case "500" or "405":
                    ModelState.AddModelError("ServerError", "We were not able to process this request. Please contact the devs");
                    break;
                case "403" or "401":
                    ModelState.AddModelError("AuthenticationError", "You are not logged in or you do not have the permission to do this action");
                    break;
                default:
                    ModelState.AddModelError("UnknownError", "An unknown error occurred");
                    break;
            }

            return Json(ModelState);
        }
    }
}
