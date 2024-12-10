using Messenger.Backend.Dto.Auth;
using Messenger.Backend.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using LoginRequest = Messenger.Backend.Dto.Auth.LoginRequest;

namespace Messenger.Backend.Controller;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthController(IUserService userService, IJwtTokenService jwtTokenService)
    {
        _userService = userService;
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _userService.Authenticate(request.Username, request.Password);
        if (user == null) return Unauthorized("Invalid credentials");

        var token = _jwtTokenService.GenerateToken(user);
        return Ok(new { Token = token });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        _userService.Register(request.Username, request.Password);
        return Ok("User registered succesfully");
    }
}