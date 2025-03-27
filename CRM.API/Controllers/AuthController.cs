using CRM.Application.DTOs;
using CRM.Application.Interfaces;
using CRM.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly TokenService _tokenService;

    public AuthController(IUserService userService, TokenService tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserDto user)
    {
        await _userService.RegisterAsync(user);
        return Ok(new { message = "User registered successfully." });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserDto loginDto)
    {
        var user = await _userService.AuthenticateAsync(loginDto.Username, loginDto.Password);
        if (user == null) return Unauthorized("Invalid credentials");

        var token = _tokenService.GenerateToken(user);
        return Ok(new { token });
    }
}
