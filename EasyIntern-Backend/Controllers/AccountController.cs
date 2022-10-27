using System.Diagnostics;
using Data;
using Microsoft.AspNetCore.Mvc;

namespace EasyIntern_Backend.Controllers;
[Route("account")]
public class AccountController : Controller
{
    private readonly Context _context;

    public AccountController(Context context)
    {
        _context = context;
    }

    [Route("")]
    public IActionResult Index()
    {
        return Json(new { yesyes = "goodshit"});
    }
}