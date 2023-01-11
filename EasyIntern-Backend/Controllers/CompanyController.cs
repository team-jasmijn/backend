using Data;
using Data.Enums;
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
        [IsCompany]
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            if (User.IsModerator()) //if the user is moderator return all companies
            {
                return Json(await _context.Users.Where(e => e.UserType == UserType.Company).ToListAsync());
            }

            if (!User.IsStudent())//user is company, return all students that have flirted with the users company.
            {

                int userId = User.Id();
                return Json(
                    (await _context.Flirts
                        .Where(flirt => flirt.CompanyId == userId && flirt.Status == FlirtStatus.StudentFlirted)
                        .Include(flirt => flirt.Student).ToListAsync()).Select(flirt => flirt.Student
                    ));
            }

            return Json(Array.Empty<User>()); //return an empty array
        }


        [IsStudent]
        [HttpGet("match")]
        [HttpGet("{page:int}")]
        public async Task<IActionResult> Match(int? page)
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
        [HttpPost("approve/{id:int}")]
        public async Task<IActionResult> ToggleApproved(int id)
        {
            var company = _context.Users.SingleOrDefault(e => e.UserType == UserType.Company && e.Id == id);

            if (company == null)
            {
                return NotFound();
            }

            company.Approved = !company.Approved;

            _context.Users.Update(company);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}