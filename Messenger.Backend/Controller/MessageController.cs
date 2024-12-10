using Messenger.Backend.Entity;
using Messenger.Backend.Repository.Common.Interfaces;
using Messenger.Backend.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Backend.Controller;

[ApiController]
[Route("api/[controller]")]
public class MessageController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;   

    public MessageController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMessages()
    {
        var messages = await _unitOfWork.Messages.GetAllAsync();
        return Ok(messages);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMessageById(Guid id)
    {
        var message = await _unitOfWork.Messages.GetByIdAsync(id);
        
        if (message == null)
        {
            return NotFound(new { Message = "Message not found" });
        }

        return Ok(message);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMessage([FromBody] Message message)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        await _unitOfWork.Messages.AddAsync(message);
        await _unitOfWork.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetMessageById), new { id = message.Id }, message);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMessage(Guid id, [FromBody] Message updatedMessage)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var message = await _unitOfWork.Messages.GetByIdAsync(id);
        if (message == null)
        {
            return NotFound(new { Message = "Message not found" });
        }

        await _unitOfWork.Messages.UpdateAsync(updatedMessage);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMessage(Guid id)
    {
        var message = await _unitOfWork.Messages.GetByIdAsync(id);
        if (message == null)
        {
            return NotFound(new { Message = "Message not found" });
        }

        await _unitOfWork.Messages.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }   
}