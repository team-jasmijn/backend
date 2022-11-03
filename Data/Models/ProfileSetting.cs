using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Data.Models;

[Index(new string[] { nameof(Id), nameof(CreateDate), nameof(ModifyDate), nameof(Value), nameof(Key) })]
public class ProfileSetting
{
    [Key]
    public int Id { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreateDate { get; set; }
    public DateTime? ModifyDate { get; set; }
    [MaxLength(50)]
    public string Key { get; set; }
    public string Value { get; set; }
    public int UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public User User { get; set; }

    public int? ProfileSettingOptionId { get; set; }
    [ForeignKey(nameof(ProfileSettingOptionId))]
    public ProfileSettingOption ProfileSettingOption { get; set; }
}