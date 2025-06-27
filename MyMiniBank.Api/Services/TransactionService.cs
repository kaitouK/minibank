using MyMiniBank.Api.Models.DataBaseContext;
using MyMiniBank.Api.Models.DTOs;
using MyMiniBank.Api.Services.Interface;
using Microsoft.EntityFrameworkCore;
using MyMiniBank.Api.Models.Entities;

namespace MyMiniBank.Api.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<TransactionService> _logger;
        public TransactionService(AppDbContext context, ILogger<TransactionService> logger)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<ApiResponse<bool>> TransferAsync(TransferRequest request)
        {
            if (request.Amount <= 0)
            {
                return ApiResponse<bool>.Fail("轉帳金額必須大於 0 ");
            }
            var fromAccount = await _context.BankAccounts
                .FirstOrDefaultAsync(a => a.AccountNumber == request.FromAccountNumber&& a.Status == AccountStatus.Active);
            var toAccount = await _context.BankAccounts
                .FirstOrDefaultAsync(a => a.AccountNumber == request.ToAccountNumber && a.Status == AccountStatus.Active);
            if (fromAccount == null|| toAccount == null)
            {
                return ApiResponse<bool>.Fail("轉帳失敗，請確認帳號是否正確或已啟用",404);
            }
            
            if(fromAccount.Id==toAccount.Id)
            {
                return ApiResponse<bool>.Fail("轉帳失敗，無法將金額轉至同一帳戶");
            }

            if(fromAccount.Balance < request.Amount)
            {
                return ApiResponse<bool>.Fail("轉帳失敗，餘額不足");
            }

            // Start a transaction to ensure atomicity
            await using var transaction = await _context.Database.BeginTransactionAsync();
            

            try
            {
                fromAccount.Balance -= request.Amount;
                toAccount.Balance += request.Amount;

                fromAccount.UpdatedAt = DateTime.UtcNow;
                toAccount.UpdatedAt = DateTime.UtcNow;

                var transferTransaction = new TransferTransaction
                {
                    FromAccountId = fromAccount.Id,
                    ToAccountId = toAccount.Id,
                    Amount = request.Amount,
                    Description = request.Description,
                    TransferredAt = DateTime.UtcNow
                };
                _context.TransferTransactions.Add(transferTransaction);// Add the transfer transaction to the context
                _context.BankAccounts.Update(fromAccount);
                _context.BankAccounts.Update(toAccount);// Update both accounts in the context

                await _context.SaveChangesAsync();// Save changes to the database
                await transaction.CommitAsync();// Commit the transaction
                return ApiResponse<bool>.Ok(true, "轉帳成功");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return ApiResponse<bool>.Fail($"資料庫錯誤： {ex.Message}", 500);
            }
        }
    }
}