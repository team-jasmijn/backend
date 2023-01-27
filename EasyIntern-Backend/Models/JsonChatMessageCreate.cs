using System.ComponentModel.DataAnnotations;

namespace EasyIntern_Backend.Models;

public class JsonChatMessageCreate
{
    [Required]
    public string Message { get; set; }

}
