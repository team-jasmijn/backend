﻿using Data;
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

        [Authorize]
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            if (User.IsModerator()) //if the user is moderator return all companies
            {
                return Json(await _context.Users.Where(e => e.UserType == UserType.Company).ToListAsync());
            }

            if (User.IsStudent())
            {
                return Unauthorized();
            }
            
            int userId = User.Id();
            return Json(
                (await _context.Flirts
                    .Where(flirt => flirt.CompanyId == userId && flirt.Status == FlirtStatus.Sent)
                    .Include(flirt => flirt.Student).ToListAsync()).Select(flirt => flirt.Student
                ));
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

            // Get a random company for now
            IQueryable<User> users = _context.Users.AsNoTracking()
                .Include(e => e.ProfileSettings)
                .Where(e => e.UserType == UserType.Company && e.Approved);
            

            List<User> companies = await users.ToListAsync();

            var random = new Random();

            return Json(companies.OrderBy(_ => random.Next()).ToList().Select(e => new
            {
                e.Name,
                e.Email,
                e.Id,
            }).First());
        }

        [IsModerator]
        [HttpPost("{id:int}/approve")]
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