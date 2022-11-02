namespace EasyIntern_Backend.Models;

public class JsonModelRegisterUser
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string RepeatPassword { get; set; }
    public string Goals { get; set; }
    public string Description { get; set; }
    public string Experience { get; set; }
}