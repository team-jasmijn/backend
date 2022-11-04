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
        
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            int userId = User.Id();
            User user = await _context.Users.Include(e => e.ProfileSettings).AsNoTracking().FirstOrDefaultAsync(e => e.Id == userId);
            if (user == null) return BadRequest("User was not found");
            return Ok(new
            {
                Education = user.ProfileSettings.FirstOrDefault(e => e.Key == "Education")?.Value,
                EducationLevel = user.ProfileSettings.FirstOrDefault(e => e.Key == "EducationLevel")?.Value,
                Skills = user.ProfileSettings.FirstOrDefault(e => e.Key == "Skills")?.Value,
                Goals = user.ProfileSettings.FirstOrDefault(e => e.Key == "Goals")?.Value,
                Name = user.Name,
                Email = user.Email
            });
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] Dictionary<string, string> model)
        {
            
            int userId = User.Id();
            User user = await _context.Users.Include(e => e.ProfileSettings)
                .FirstOrDefaultAsync(e => e.Id == userId);
            if (user == null) return BadRequest();

            if (model.ContainsKey("email")) model.Remove("email");

            if (model.ContainsKey("name"))
            {
                user.Name = model["name"];
                model.Remove("name");
            }

            foreach (KeyValuePair<string, string> pair in model)
            {
                if (user.ProfileSettings.Any(e => e.Key == pair.Key))
                {
                    user.ProfileSettings.First(e => e.Key == pair.Key).Value = pair.Value;
                }
                else
                {
                    user.ProfileSettings.Add(new ProfileSetting()
                    {
                        Key = pair.Key,
                        Value = pair.Value
                    });
                }

            }

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
