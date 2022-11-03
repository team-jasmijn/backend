using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyIntern_Backend.Areas.Student.Controllers
{
    [Route("student/account"), Area("Student")]
    public class AccountController : Controller
    {
        private Context _context;

        public AccountController(Context context)
        {
            _context = context;
        }

        [Route("")]
        [Authorize]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
