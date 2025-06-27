namespace MyMiniBank.Api.Models.DTOs
{
    /// <summary>
    /// Represents a request to transfer money between accounts.
    /// This DTO is used to encapsulate the details of a transfer operation.
    /// </summary>
    public class TransferRequest
    {
        public required string FromAccountNumber { get; set; } // The ID of the account from which the money is being transferred
        public required string ToAccountNumber { get; set; } // The ID of the account to which the money is being transferred
        public decimal Amount { get; set; } // The amount of money to be transferred
        public string? Description { get; set; } // Optional description of the transfer
    }
}