using System.ComponentModel.DataAnnotations;

namespace EasyIntern_Backend.Models;

public class JsonFlirtCreate
{
    [Required]
    public int CompanyId { get; set; }
}