namespace MyMiniBank.Api.Models.DTOs
{
    /// <summary>
    /// Represents a summary of a bank account.
    /// </summary>
    public class BankAccountSummaryRequest
    {
       
        /// <summary>
        /// Gets the Number of the bank account.
        /// </summary>
        public string AccountNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets the current balance of the bank account.
        /// </summary>
        public decimal Balance { get; set; }
         /// <summary>
        /// Gets the type of the bank account.
        /// </summary>
        public string AccountType { get; set; } = string.Empty;
        /// <summary>
        /// Gets the status of the bank account.
        /// </summary>
        public string Status { get; set; } = string.Empty;
    }
}