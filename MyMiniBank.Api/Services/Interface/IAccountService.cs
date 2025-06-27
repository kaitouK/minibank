using MyMiniBank.Api.Models.DTOs;

namespace MyMiniBank.Api.Services.Interface
{
    public interface IAccountService
    {
        /// <summary>
        /// Retrieves a summary of all bank accounts for the user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A list of bank account summaries.</returns>
        Task<ApiResponse<List<BankAccountSummaryRequest>>> GetAccountsAsync(int userId);
    }
}