using System.ComponentModel.DataAnnotations;

namespace EasyIntern_Backend.Models;

public class JsonModelFlirtCreate
{
    [Required]
    public int CompanyId { get; set; }
}