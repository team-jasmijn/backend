using System.ComponentModel.DataAnnotations;

namespace EasyIntern_Backend.Models;

public class JsonModelRegisterCompany
{
    [Required]
    public string CompanyName { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    
    public string RepeatPassword { get; set; }

    public string Description { get; set; }
    public string[] Files { get; set; }
    public string Education { get; set; }

}