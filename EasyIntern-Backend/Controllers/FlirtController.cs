using Data;
using Data.Enums;
using Data.Helpers;
using Data.Models;
using EasyIntern_Backend.Attributes;
using EasyIntern_Backend.Models;
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
                .Where(e => e.Status != FlirtStatus.Accepted)
                .Include(e => e.Student)
                .ThenInclude(e => e.ProfileSettings).Where(e => e.CompanyId == userId)
                .ToListAsync();

            var selectedFlirts = flirts.Select(e => new
            {
                e.Id,
                Student = new
                {
                    e.Student.Name,
                    ProfileSettings = new Dictionary<string, string>(
                        e.Student.ProfileSettings.Select(o => new KeyValuePair<string, string>(o.Key, o.Value)))
                }
            });

            return Json(selectedFlirts.ToList());
        }


        [IsStudent]
        [HttpPost("")]
        public async Task<IActionResult> FlirtCompany([FromBody] JsonFlirtCreate model)
        {
            int studentId = User.Id();
            bool flirtExists = await _context.Flirts.AnyAsync(e => e.StudentId == studentId && e.CompanyId == model.CompanyId);

            if (flirtExists)
            {
                ModelState.AddModelError("AlreadyFlirted", "You already flirted with this company");
                return BadRequest(ModelState);
            }

            User company = await _context.Users.FirstOrDefaultAsync(e => e.Id == model.CompanyId && e.UserType.HasFlag(UserType.Company));
            if (company == null)
            {
                ModelState.AddModelError("UserNotFound", "Company was not found");
                return NotFound(ModelState);
            }

            Flirt flirt = new Flirt()
            {
                CompanyId = model.CompanyId,
                Status = FlirtStatus.Sent,
                CreateDate = DateTime.UtcNow,
                StudentId = studentId
            };
            _context.Flirts.Add(flirt);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [IsCompany]
        [HttpPost("{flirtId:int}/reject")]
        public async Task<IActionResult> DenyStudent(int flirtId)
        {
            int userId = User.Id();
            Flirt flirt = await _context.Flirts.FirstOrDefaultAsync(e =>
                e.Id == flirtId && e.CompanyId == userId && e.Status == FlirtStatus.Sent);
            if (flirt == null)
            {
                ModelState.AddModelError("FlirtNotFound", "Flirt was not found");
                return NotFound(ModelState);
            }

            flirt.Status = FlirtStatus.Rejected;
            _context.Flirts.Update(flirt);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [IsCompany]
        [HttpPost("{flirtId:int}/accept")]
        public async Task<IActionResult> AcceptStudent(int flirtId)
        {
            int userId = User.Id();
            Flirt flirt = await _context.Flirts.FirstOrDefaultAsync(e => e.Id == flirtId && e.CompanyId == userId && e.Status == FlirtStatus.Sent);
            if (flirt == null)
            {
                ModelState.AddModelError("FlirtNotFound", "Flirt was not found");
                return NotFound(ModelState);
            }
            flirt.Status = FlirtStatus.Accepted;
            _context.Flirts.Update(flirt);
            
            _context.Chats.Add(new Chat()
            {
                StudentId = flirt.StudentId,
                CompanyId = flirt.CompanyId
            });
            
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}