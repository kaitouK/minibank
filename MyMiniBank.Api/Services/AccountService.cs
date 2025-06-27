using MyMiniBank.Api.Models.DataBaseContext;
using MyMiniBank.Api.Models.DTOs;
using MyMiniBank.Api.Services.Interface;
using MyMiniBank.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;
namespace MyMiniBank.Api.Services
{
    public class AccountService : IAccountService
    {
        private readonly AppDbContext _context;

        public AccountService(AppDbContext context)
        {  
            _context = context;  
        }


        public async Task<ApiResponse<List<BankAccountSummaryRequest>>> GetAccountsAsync(int userId)
        {
            try
            {
                var accounts = await _context.BankAccounts
                .Where(a => a.UserId == userId && a.Status == AccountStatus.Active)
                .Select(a => new BankAccountSummaryRequest
                {
                    AccountNumber = a.AccountNumber,
                    Balance = a.Balance,
                    AccountType = a.AccountType.ToString(),
                    Status = a.Status.ToString()
                })
                .ToListAsync();

                return ApiResponse<List<BankAccountSummaryRequest>>.Ok(accounts, "帳戶餘額查詢成功");
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                return ApiResponse<List<BankAccountSummaryRequest>>.Fail("帳戶餘額查詢失敗: " + ex.Message);
            }
        }
    }
}