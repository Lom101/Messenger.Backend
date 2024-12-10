using Messenger.Backend.Entity;
using Messenger.Backend.Repository.Common.Interfaces;
using Messenger.Backend.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Backend.Controller;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;   

    public UserController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _unitOfWork.Users.GetAllAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(id);
        
        if (user == null)
        {
            return NotFound(new { Message = "User not found" });
        }

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] User updatedUser)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var user = await _unitOfWork.Users.GetByIdAsync(id);
        if (user == null)
        {
            return NotFound(new { Message = "User not found" });
        }

        await _unitOfWork.Users.UpdateAsync(updatedUser);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(id);
        if (user == null)
        {
            return NotFound(new { Message = "User not found" });
        }

        await _unitOfWork.Users.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }   
}