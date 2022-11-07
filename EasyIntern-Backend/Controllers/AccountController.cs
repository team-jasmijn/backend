﻿using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Data;
using Data.Enums;
using Data.Helpers;
using Data.Models;
using EasyIntern_Backend.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EasyIntern_Backend.Controllers;
[Route("account")]
public class AccountController : Controller
{
    private IConfiguration _config;
    private Context _context;
    public AccountController(IConfiguration config, Context context)
    {
        _config = config;
        _context = context;
    }

    [HttpPost("register-student")]
    public async Task<IActionResult> RegisterStudent([FromBody] JsonModelRegisterStudent model )
    {
        if (model.Password != model.RepeatPassword) return BadRequest("Passwords do not match");
        if (await _context.Users.AnyAsync(e => e.Email == model.Email)) return BadRequest("A user has already been found using this email");

        var user = new User()
        {
            Name = model.Name,
            Email = model.Email,
            TimeZoneId = "Africa/Abidjan",
            UserType = UserType.Student,
            ProfileSettings = new List<ProfileSetting>()
            {
                new ProfileSetting()
                {
                    Key = nameof(model.Description),
                    Value = model.Description
                },
                 new ProfileSetting()
                 {
                     Key = nameof(model.Experience),
                     Value = model.Experience
                 },
                 new ProfileSetting()
                 {
                      Key = nameof(model.Education),
                      Value = model.Education,
                 },
                 new ProfileSetting()
                 {
                     Key = nameof(model.Goals),
                     Value = model.Goals
                 },
                 new ProfileSetting()
                 {
                     Key = nameof(model.EducationLevel),
                     Value = model.EducationLevel.ToString()
                 },
                 new ProfileSetting()
                 {
                     Key = nameof(model.School),
                     Value = model.School
                 }
            }
        };

        BCryptHelper.ConfigureUserPassword(user, model.Password);


        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("register-company")]
    public async Task<IActionResult> RegisterCompany([FromBody] JsonModelRegisterCompany model)
    {
        if (model.Password != model.RepeatPassword) return BadRequest("Passwords do not match");
        if (await _context.Users.AnyAsync(e => e.Email == model.Email)) return BadRequest("A user has already been found using this email");

        var user = new User()
        {
            Name = model.CompanyName,
            Email = model.Email,
            TimeZoneId = "Africa/Abidjan",
            UserType = UserType.Company,
            ProfileSettings = new List<ProfileSetting>()
            {
                new ProfileSetting()
                {
                    Key = nameof(model.Description),
                    Value = model.Description
                },
                new ProfileSetting()
                {
                    Key = nameof(model.Education),
                    Value = model.Education
                }
            }
        };
        BCryptHelper.ConfigureUserPassword(user, model.Password);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("validate")]
    [Authorize]
    public IActionResult Validate() => Ok(); //check if user is logged in

    [HttpPost("login")]
    public async Task<IActionResult> Login([Bind("Email,Hash")][FromBody] User user)
    {
        const string safeReturnError = "Email and password do not match";
        User dbUser = await _context.Users.FirstOrDefaultAsync(e => e.Email == user.Email);
        if (dbUser == null) return BadRequest(safeReturnError);
        if (!BCryptHelper.ValidatePassword(dbUser, user.Hash)) 
            return BadRequest(safeReturnError);
        string token = GetJwtToken(dbUser);
        return Ok(token);
    }

    [HttpGet("")]
    [Authorize]
    public async Task<IActionResult> GetProfileData()
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
    [Authorize]
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

    [NonAction]
    private string GetJwtToken(User user)
    {
        string issuer = _config["Jwt:Issuer"];
        string audience = _config["Jwt:Audience"];
        byte[] key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Email", user.Email),
                new Claim("UserType", user.UserType.ToString()),
                new Claim("timeZoneId", user.TimeZoneId ?? "")
            }),
            Expires = DateTime.MaxValue,
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        string stringToken = tokenHandler.WriteToken(token);
        return stringToken;
    }
}