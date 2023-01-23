using System.Diagnostics;
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

    [HttpPost("register")]
    public async Task<IActionResult> RegisterStudent([FromBody] JsonModelRegisterUser model )
    {
        if (User.Identity?.IsAuthenticated is true) ModelState.AddModelError("Authentication", "User is already logged in");
        if (model.Password != model.RepeatPassword) ModelState.AddModelError("RepeatPassword", "Passwords do not match");
        if (model.UserType != UserType.Company && model.UserType != UserType.Student) ModelState.AddModelError("UserType", "UserType is not allowed");
        if (await _context.Users.AnyAsync(e => e.Email == model.Email)) ModelState.AddModelError("Email","A user has already been found using this email");
        //check if the model is correct
        if (!ModelState.IsValid)
        {
            return Json(ModelState);
        }
        //predefine the users settings and fill it if the data is not empty
        List<ProfileSetting> profileSettings = new()
        {
            new ProfileSetting()
            {
                Key = nameof(model.EducationLevel),
                Value = model.EducationLevel.ToString()
            }
        };
        
        if (!string.IsNullOrEmpty(model.Education))
        {
            profileSettings.Add(new ProfileSetting()
            {
                Key = nameof(model.Education),
                Value = model.Education
            });
        }
        
        if (!string.IsNullOrEmpty(model.Experience))
        {
            profileSettings.Add(new ProfileSetting()
            {
                Key = nameof(model.Experience),
                Value = model.Experience
            });
        }
        
        if (!string.IsNullOrEmpty(model.Goals))
        {
            profileSettings.Add(new ProfileSetting()
            {
                Key = nameof(model.Goals),
                Value = model.Goals
            });
        }
        
        if (!string.IsNullOrEmpty(model.Description))
        {
            profileSettings.Add(new ProfileSetting()
            {
                Key = nameof(model.Description),
                Value = model.Description
            });
        }

        if (!string.IsNullOrEmpty(model.School))
        {
            profileSettings.Add(new ProfileSetting()
            {
                Key = nameof(model.School),
                Value = model.School
            });
        }
        
        var user = new User()
        {
            Name = model.Name,
            Email = model.Email,
            TimeZoneId = "Africa/Abidjan",
            UserType = model.UserType,
            Approved = model.UserType == UserType.Student, //approve only as student
            ProfileSettings = profileSettings
        };
        //hashes the users password and creates a salt
        BCryptHelper.ConfigureUserPassword(user, model.Password);


        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        //insert the user
        return Ok();
    }

    [HttpGet("validate")]
    [Authorize]
    public IActionResult Validate() => Ok(); //check if user is logged in

    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] JsonModelAccountLogin model)
    {
        const string safeReturnError = "Email and password do not match"; //return a safe error string to prevent brute force attacks
        User dbUser = await _context.Users.FirstOrDefaultAsync(e => e.Email == model.Email);
        if (dbUser == null) ModelState.AddModelError("AuthenticationError", safeReturnError);
        if (!BCryptHelper.ValidatePassword(dbUser, model.Password)) ModelState.AddModelError("AuthenticationError", safeReturnError);
        //check if the sent model is valid
        if (!ModelState.IsValid)
        {
            return Json(ModelState);
        }
        string token = GetJwtToken(dbUser);
        return Ok(token);
    }

    [HttpGet("")]
    [Authorize]
    public async Task<IActionResult> GetProfileData()
    {
        int userId = User.Id();
        User user = await _context.Users.Include(e => e.ProfileSettings).AsNoTracking().FirstOrDefaultAsync(e => e.Id == userId);
        //get the users profile data
        if (user == null)
        {
            ModelState.AddModelError("UserNotFound", "User was not found");
            return Json(ModelState);
        }
        
        return Ok(new
        {
            Education = user.ProfileSettings.FirstOrDefault(e => e.Key == "Education")?.Value,
            EducationLevel = user.ProfileSettings.FirstOrDefault(e => e.Key == "EducationLevel")?.Value,
            Skills = user.ProfileSettings.FirstOrDefault(e => e.Key == "Skills")?.Value,
            Goals = user.ProfileSettings.FirstOrDefault(e => e.Key == "Goals")?.Value,
            Name = user.Name,
            Email = user.Email,
            Role = user.UserType.ToString()
        });
    }

    [HttpPost("update")]
    [Authorize]
    public async Task<IActionResult> Update([FromBody] Dictionary<string, string> model)
    {

        int userId = User.Id();
        User user = await _context.Users.Include(e => e.ProfileSettings)
            .FirstOrDefaultAsync(e => e.Id == userId);
        if (user == null)
        {
            ModelState.AddModelError("UserNotFound", "User was not found");
            return BadRequest(ModelState);
        }
        //remove the single exception (name) from the profilesetting data
        if (model.ContainsKey("name"))
        {
            user.Name = model["name"];
            model.Remove("name");
        }

        foreach (KeyValuePair<string, string> pair in model)
        {
            //check if the profilesetting can exists
            if (!await _context.ProfilesettingsOptions.AnyAsync(e => e.Key == pair.Key))
            {
                ModelState.AddModelError("InvalidProfileSetting", $"The key {pair.Key} cannot be added to your profile");
                continue;
            }
            //check if the profilesetting already exists. If not, add it
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
        //return the profilesettings that could not be inserted/updated
        return Ok(ModelState);
    }

    //this should not be called by an endpoint in the API
    [NonAction]
    private string GetJwtToken(User user)
    {
        //do some magic that returns an valid JWT Bearer token based on the given user
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