using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models;

[Index(new string[] { nameof(Id), nameof(CreateDate), nameof(ModifyDate)})]
public class User
{
    [Key]
    public int Id { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreateDate { get; set; }
    public DateTime? ModifyDate { get; set; }

    public string TimeZoneId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Hash { get; set; }
    public string Salt { get; set; }


    public List<File> Files { get; set; }
    public List<Flirt> Flirts { get; set; }
    public List<Chat> Chats { get; set; }
    public List<ChatMessage> SentChatMessages { get; set; }
    public List<ProfileSetting> ProfileSettings { get; set; }
    public List<UserRole> Roles { get; set; }
}