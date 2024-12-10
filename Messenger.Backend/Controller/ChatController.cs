using Messenger.Backend.Entity;
using Messenger.Backend.Repository.Common;
using Messenger.Backend.Repository.Common.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Backend.Controller;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    public ChatController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllChats()
    {
        var chats = await _unitOfWork.Chats.GetAllAsync();
        return Ok(chats);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetChatById(Guid id)
    {
        var chat = await _unitOfWork.Chats.GetByIdAsync(id);
        if (chat == null)
            return NotFound(new { Message = "Chat not found" });

        return Ok(chat);
    }

    [HttpPost]
    public async Task<IActionResult> CreateChat([FromBody] Chat chat)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _unitOfWork.Chats.AddAsync(chat);
        await _unitOfWork.SaveChangesAsync();

        return CreatedAtAction(nameof(GetChatById), new { id = chat.Id }, chat);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateChat(Guid id, [FromBody] Chat updatedChat)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var chat = await _unitOfWork.Chats.GetByIdAsync(id);
        if (chat == null)
        {
            return NotFound(new { Message = "Chat not found" });
        }

        await _unitOfWork.Chats.UpdateAsync(updatedChat);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteChat(Guid id)
    {
        var chat = await _unitOfWork.Chats.GetByIdAsync(id);
        if (chat == null)
        {
            return NotFound(new { Message = "Chat not found" });
        }

        await _unitOfWork.Chats.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }
}