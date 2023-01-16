using Data;
using Data.Helpers;
using Data.Models;
using EasyIntern_Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyIntern_Backend.Controllers
{
  [Route("chat"), Authorize]
  public class ChatController : Controller
  {
    private readonly Context _context;

    public ChatController(Context context)
    {
      _context = context;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Index(int id)
    {
      int userId = User.Id();
      var chat = await _context.Chats.AsNoTracking()
        .Include(e => e.ChatMessages)
        .Include(e => e.Student)
        .Include(e => e.Company)
        .SingleOrDefaultAsync(e => e.Id == id && (e.CompanyId == userId || e.StudentId == userId));
      if (chat == null)
      {
        return NotFound();
      }
      return Json(chat);
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
      int userId = User.Id();
      var chats = await _context.Chats.AsNoTracking()
        .Include(e => e.ChatMessages)
        .Include(e => e.Student)
        .Include(e => e.Company)
        .Where(e => e.CompanyId == userId || e.StudentId == userId).ToListAsync();
      return Json(chats);
    }

    [HttpPost("{id:int}/message")]
    public async Task<IActionResult> SendMessage(int id, [FromBody] JsonChatMessageCreate model)
    {
      int userId = User.Id();
      var chat = await _context.Chats.AsNoTracking()
        .Include(e => e.ChatMessages)
        .Include(e => e.Student)
        .Include(e => e.Company)
        .SingleOrDefaultAsync(e => e.Id == id && (e.CompanyId == userId || e.StudentId == userId));
      if (chat == null)
      {
        return NotFound();
      }

      ChatMessage chatMessage = new ChatMessage()
      {
        ChatId = id,
        Message = model.Message,
        SenderId = userId,
      };

      _context.ChatMessages.Add(chatMessage);
      await _context.SaveChangesAsync();
      return Json(chatMessage);
    }
  }
}