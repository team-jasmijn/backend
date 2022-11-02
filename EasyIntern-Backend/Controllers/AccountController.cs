using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Data;
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
    public async Task<IActionResult> Register([FromBody] JsonModelRegisterUser model )
    {
        if (model.Password != model.RepeatPassword) return BadRequest("Passwords do not match");

        var user = new User()
        {
            Name = model.Name,
            Email = model.Email,
            TimeZoneId = "Africa/Abidjan"
        };
        BCryptHelper.ConfigureUserPassword(user, model.Password);

        if (await _context.Users.AnyAsync(e => e.Email == model.Email)) return BadRequest("A user has already been found using this email");

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        string token = GetJwtToken(user);
        return Ok(token);
    }

    [HttpGet("validate")]
    [Authorize]
    public IActionResult Validate() => Ok(); //check if user is logged in

    [HttpPost("login")]
    public async Task<IActionResult> Login([Bind("Email,Hash")][FromBody] User user)
    {
        const string safeReturnError = "User and email do not match";
        User dbUser = await _context.Users.FirstOrDefaultAsync(e => e.Email == user.Email);
        if (dbUser == null) return BadRequest(safeReturnError);
        if (!BCryptHelper.ValidatePassword(dbUser, user.Hash)) 
            return BadRequest(safeReturnError);
        string token = GetJwtToken(dbUser);
        return Ok(token);
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
                new Claim("Name", user.Name),
                new Claim("Email", user.Email),
                new Claim("timeZoneId", user.TimeZoneId)
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