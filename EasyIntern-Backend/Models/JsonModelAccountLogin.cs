using System.ComponentModel.DataAnnotations;

namespace EasyIntern_Backend.Models;

public class JsonModelAccountLogin
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }

}