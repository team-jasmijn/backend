using Data;
using Data.Enums;
using Data.Helpers;
using Data.Models;
using EasyIntern_Backend.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace EasyIntern_Backend.Controllers
{
    [Route("flirts")]
    public class FlirtController : Controller
    {
        private readonly Context _context;

        public FlirtController(Context context)
        {
            _context = context;
        }

        [IsCompany]
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            int userId = User.Id();
            var flirts = await _context.Flirts.AsNoTracking()
                .Include(e => e.Student)
                .ThenInclude(e => e.ProfileSettings).Where(e => e.CompanyId == userId).ToListAsync();
            return Json(flirts.Select(e => new
            {
                e.Id,
                Student = new
                {
                    e.Student.Name,
                    ProfileSettings = e.Student.ProfileSettings.Select(o => new
                    {
                        o.Key,
                        o.Value
                    })
                }
            }));
        }


        [IsStudent]
        [HttpPost("flirt-company/{companyId:int}")]
        public async Task<IActionResult> FlirtCompany(int companyId)
        {
            User company = await _context.Users.FirstOrDefaultAsync(e => e.Id == companyId && e.UserType.HasFlag(UserType.Company));
            if (company == null) return NotFound();
            Flirt flirt = new Flirt()
            {
                CompanyId = companyId,
                Status = FlirtStatus.StudentFlirted,
                CreateDate = DateTime.UtcNow,
                StudentId = User.Id()
            };
            _context.Flirts.Add(flirt);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [IsCompany]
        [HttpPost("flirt-student/{flirtId:int}")]
        public async Task<IActionResult> FlirtStudent(int flirtId)
        {
            int userId = User.Id();
            Flirt flirt = await _context.Flirts.FirstOrDefaultAsync(e => e.Id == flirtId && e.CompanyId == userId && e.Status == FlirtStatus.StudentFlirted);
            if (flirt == null) return NotFound();
            flirt.Status = FlirtStatus.CompanyFlirted;
            Chat chat = new Chat()
            {
                CompanyId = flirt.CompanyId,
                StudentId = flirt.StudentId
            };
            _context.Chats.Add(chat);
            _context.Flirts.Update(flirt);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                chat.Id,
                chat.StudentId
            });
        }

        [IsCompany]
        [HttpPost("deny-student/{flirtId:int}")]
        public async Task<IActionResult> DenyStudent(int flirtId)
        {
            int userId = User.Id();
            Flirt flirt = await _context.Flirts.FirstOrDefaultAsync(e => e.Id == flirtId && e.CompanyId == userId && e.Status == FlirtStatus.StudentFlirted);
            if (flirt == null) return NotFound();
            flirt.Status = FlirtStatus.Finished;
            _context.Flirts.Update(flirt);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
