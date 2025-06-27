using System.ComponentModel.DataAnnotations.Schema;
namespace MyMiniBank.Api.Models.Entities
{
    /// <summary>
    /// Represents a transfer transaction between two bank accounts.
    /// </summary>
    public class TransferTransaction
    {
        public int Id { get; set; } // Unique identifier for the transfer transaction
        
        public required int FromAccountId { get; set; } // Account number from which the money is transferred
        public BankAccount FromAccount { get; set; } = null!; // Navigation property to the source bank account
        public required int ToAccountId { get; set; } // Account number to which the money is transferred
        public BankAccount ToAccount { get; set; } = null!; // Navigation property to the destination bank account
        [Column(TypeName = "REAL")] // Use REAL type for decimal in SQLite
        public decimal Amount { get; set; } // Amount of money transferred
        public string? Description { get; set; } // Optional description of the transfer
        public DateTime TransferredAt { get; set; } = DateTime.UtcNow; // Date and time of the transfer
    }
}