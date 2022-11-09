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
    [Route("student"), Authorize]
    public class StudentController : Controller
    {
        private readonly Context _context;

        public StudentController(Context context)
        {
            _context = context;
        }
        [IsCompany]
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            int userId = User.Id();
            List<Flirt> flirts = await _context.Flirts.Where(e => e.CompanyId == userId && e.Status < FlirtStatus.CompanyFlirted)
                .Include(e => e.Student).ThenInclude(e => e.ProfileSettings).ToListAsync();

            return Json(flirts.Select(e => e.Student));
        }
    }
}
