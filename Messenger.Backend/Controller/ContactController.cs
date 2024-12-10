using Messenger.Backend.Entity;
using Messenger.Backend.Repository.Common.Interfaces;
using Messenger.Backend.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Backend.Controller;

[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;   

    public ContactController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllContacts()
    {
        var contacts = await _unitOfWork.Contacts.GetAllAsync();
        return Ok(contacts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetContactById(Guid id)
    {
        var contact = await _unitOfWork.Contacts.GetByIdAsync(id);
        
        if (contact == null)
        {
            return NotFound(new { Message = "Contact not found" });
        }

        return Ok(contact);
    }

    [HttpPost]
    public async Task<IActionResult> CreateContact([FromBody] Contact contact)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        await _unitOfWork.Contacts.AddAsync(contact);
        await _unitOfWork.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetContactById), new { id = contact.Id }, contact);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateContact(Guid id, [FromBody] Contact updatedContact)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var contact = await _unitOfWork.Contacts.GetByIdAsync(id);
        if (contact == null)
        {
            return NotFound(new { Message = "Contact not found" });
        }

        await _unitOfWork.Contacts.UpdateAsync(updatedContact);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContact(Guid id)
    {
        var contact = await _unitOfWork.Contacts.GetByIdAsync(id);
        if (contact == null)
        {
            return NotFound(new { Message = "Contact not found" });
        }

        await _unitOfWork.Contacts.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }   
}