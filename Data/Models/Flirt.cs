using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Data.Enums;

namespace Data.Models;

[Index(new string[] { nameof(Id), nameof(CreateDate), nameof(ModifyDate) })]
[Index(new string[] { nameof(CompanyId), nameof(StudentId) }, IsUnique = true)]
public class Flirt
{
    // A flirt is basically just a message request
    [Key]
    public int Id { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreateDate { get; set; }
    public DateTime? ModifyDate { get; set; }
    public FlirtStatus Status { get; set; }

    public int CompanyId { get; set; }
    [ForeignKey(nameof(CompanyId))]
    public User Company { get; set; }

    public int StudentId { get; set; }
    [ForeignKey(nameof(StudentId))]
    public User Student { get; set; }
}