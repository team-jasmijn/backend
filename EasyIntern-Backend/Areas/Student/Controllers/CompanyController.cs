using Data;
using Data.Helpers;
using Data.Models;
using EasyIntern_Backend.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyIntern_Backend.Areas.Student.Controllers
{
    [Route("student/company"), Authorize, Area("Student"), IsStudent]
    public class CompanyController : Controller
    {
        private readonly Context _context;
        private const int takeAmount = 2;
        public CompanyController(Context context)
        {
            _context = context;
        }
        [HttpGet("")]
        [HttpGet("{page:int}")]
        public async Task<IActionResult> Index(int? page)
        {
            int userId = User.Id();
            User student = await _context.Users.AsNoTracking()
                .Include(e => e.ProfileSettings)
                .SingleOrDefaultAsync(u => u.Id == userId);
            if (student == null) return BadRequest();
            var filter = DynamicFiltersHelper.GenerateMatchingFilterForCompany(student);
            IQueryable<User> users = _context.Users.Where(filter);
            List<User> companies = await users.Skip(takeAmount * (page ?? 0)).Take(takeAmount).ToListAsync();
            return Json(companies);
        }
    }
}
