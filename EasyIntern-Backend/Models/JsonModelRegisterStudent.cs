using System.ComponentModel.DataAnnotations;

namespace EasyIntern_Backend.Models;

public class JsonModelRegisterStudent
{
    
    public string Name { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string RepeatPassword { get; set; }
    public string Goals { get; set; }
    public string Description { get; set; }
    public string Experience { get; set; }
    public string Education { get; set; }
    public int EducationLevel { get; set; }
    public string School { get; set; }
}