using Microsoft.AspNetCore.Authorization;
using MyMiniBank.Api.Models.DTOs;
using MyMiniBank.Api.Services.Interface;
using MyMiniBank.Api.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountsController(IAccountService accountService, IHttpContextAccessor httpContextAccessor)
    {
        _accountService = accountService;
    }

    [HttpGet("balance")]
    public async Task<IActionResult> GetMyBalances()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        try
        {
            if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
            {
                // 如果找不到 Claim 或者轉換失敗，說明無法取得有效的使用者 ID
                return Unauthorized(ApiResponse<string>.Fail("無法取得有效的使用者 ID 或格式不正確。"));
            }

            var result = await _accountService.GetAccountsAsync(userId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Log the exception (not implemented here)
            return StatusCode(500, ApiResponse<string>.Fail("帳戶餘額查詢失敗: " + ex.Message));
        }
        /*if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
        {
            return Unauthorized(ApiResponse<string>.Fail("無法取得使用者資訊"));
        }

        var result = await _accountService.GetAccountsAsync(userId);
        return Ok(result);*/
    }
}