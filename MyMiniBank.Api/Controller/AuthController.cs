using Microsoft.AspNetCore.Mvc;
using MyMiniBank.Api.Services.Interface;
using MyMiniBank.Api.Models.DTOs;
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var result = await _userService.LoginAsync(request);
        if (result.Success)
        {
            return Ok(result);
        }
        return Unauthorized(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var result = await _userService.RegisterAsync(request);
        if (result.Success)
        {
            return CreatedAtAction(nameof(Login), new { email = request.Email }, result);
        }
        return BadRequest(result);
    }
}