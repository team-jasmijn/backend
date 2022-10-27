using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Data.Models;
[Index(new string[] { nameof(Id), nameof(CreateDate), nameof(ModifyDate) })]

public class ProfileSettingOption
{
    [Key]
    public int Id { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreateDate { get; set; }
    public DateTime? ModifyDate { get; set; }
    [MaxLength(50)]
    public string Key { get; set; }
    public string Value { get; set; }
    public List<ProfileSetting> ProfileSettings { get; set; }
}