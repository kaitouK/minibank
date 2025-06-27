using Microsoft.AspNetCore.Mvc;
using MyMiniBank.Api.Services.Interface;
using MyMiniBank.Api.Models.DTOs;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }

    
}