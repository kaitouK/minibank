using MyMiniBank.Api.Models.DTOs;
namespace MyMiniBank.Api.Services.Interface
{
    /// <summary>
    /// Defines the contract for transaction services in the banking application.
    /// This interface provides methods for creating transactions and retrieving transactions by account ID.   
    /// </summary>
    public interface ITransactionService
    {
        /// <summary>
        /// Creates a new transaction.
        /// </summary>
        /// <param name="transaction">The transaction details.</param>
        /// <returns>A task representing the asynchronous operation, with a boolean indicating success.</returns>
        Task<ApiResponse<bool>> TransferAsync(TransferRequest transferRequest);
    }
}