using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Data.Models;
[Index(new string[] { nameof(Id), nameof(CreateDate), nameof(ModifyDate) })]

public class ChatMessage
{
  [Key]
  public int Id { get; set; }
  [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
  public DateTime CreateDate { get; set; }
  public DateTime? ModifyDate { get; set; }

  public int ChatId { get; set; }
  [ForeignKey(nameof(ChatId))]
  public Chat Chat { get; set; }

  public int SenderId { get; set; }
  [ForeignKey(nameof(SenderId))]
  public User Sender { get; set; }

  public string Message { get; set; }
}