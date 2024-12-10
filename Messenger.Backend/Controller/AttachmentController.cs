using Messenger.Backend.Entity;
using Messenger.Backend.Repository.Common.Interfaces;
using Messenger.Backend.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Backend.Controller;

[ApiController]
[Route("api/[controller]")]
public class AttachmentController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;   

    public AttachmentController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAttachments()
    {
        var attachments = await _unitOfWork.Attachments.GetAllAsync();
        return Ok(attachments);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAttachmentById(Guid id)
    {
        var attachment = await _unitOfWork.Attachments.GetByIdAsync(id);
        
        if (attachment == null)
        {
            return NotFound(new { Message = "Attachment not found" });
        }

        return Ok(attachment);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAttachment([FromBody] Attachment attachment)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        await _unitOfWork.Attachments.AddAsync(attachment);
        await _unitOfWork.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetAttachmentById), new { id = attachment.Id }, attachment);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAttachment(Guid id, [FromBody] Attachment updatedAttachment)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var attachment = await _unitOfWork.Attachments.GetByIdAsync(id);
        if (attachment == null)
        {
            return NotFound(new { Message = "Attachment not found" });
        }

        await _unitOfWork.Attachments.UpdateAsync(updatedAttachment);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAttachment(Guid id)
    {
        var attachment = await _unitOfWork.Attachments.GetByIdAsync(id);
        if (attachment == null)
        {
            return NotFound(new { Message = "Attachment not found" });
        }

        await _unitOfWork.Attachments.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }   
}