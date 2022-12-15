using Data;
using Data.Helpers;
using Data.Models;
using EasyIntern_Backend.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyIntern_Backend.Controllers
{
    [Route("company"), Authorize]
    public class CompanyController : Controller
    {
        private readonly Context _context;
        private const int TakeAmount = 10;
        public CompanyController(Context context)
        {
            _context = context;
        }

        [IsStudent]
        [HttpGet("match")]
        [HttpGet("{page:int}")]
        public async Task<IActionResult> Index(int? page)
        {
            int userId = User.Id();
            User student = await _context.Users.AsNoTracking()
                .Include(e => e.ProfileSettings)
                .SingleOrDefaultAsync(u => u.Id == userId);
            if (student == null)
            {
                ModelState.AddModelError("UserNotFound", "User was not found");
                return BadRequest(ModelState);
            }
            var filter = DynamicFiltersHelper.GenerateMatchingFilterForCompany(student);
            IQueryable<User> users = _context.Users.Where(filter);
            List<User> companies = await users.Skip(TakeAmount * (page ?? 0)).Take(TakeAmount).ToListAsync();

            return Json(companies.Select(e => new
            {
                e.Name,
                e.Email,
                e.Id,
            }));
        }

        [IsModerator]
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            // Return all companies
            List<User> companies = await _context.Users.AsNoTracking().Where(e => e.UserType == Data.Enums.UserType.Company).ToListAsync();
            return Json(companies.Select(e => new
            {
                e.Name,
                e.Email,
                e.Id,
            }));
        }
    }
}
