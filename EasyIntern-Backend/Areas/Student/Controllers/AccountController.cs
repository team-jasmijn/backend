using Data;
using Data.Helpers;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyIntern_Backend.Areas.Student.Controllers
{
    [Route("student/account"), Area("Student")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly Context _context;

        public AccountController(Context context)
        {
            _context = context;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            User user = await _context.Users.Include(e => e.ProfileSettings).FirstOrDefaultAsync(e => e.Id == User.Id());
            if (user == null) return BadRequest("User was not found");
            return Ok(new
            {
                Education = user.ProfileSettings.FirstOrDefault(e => e.Key == "Education")?.Value,
                EducationLevel = user.ProfileSettings.FirstOrDefault(e => e.Key == "EducationLevel")?.Value,
                Skills = user.ProfileSettings.FirstOrDefault(e => e.Key == "Skills")?.Value,
                Goals = user.ProfileSettings.FirstOrDefault(e => e.Key == "Goals")?.Value
                
            });
        }
    }
}
