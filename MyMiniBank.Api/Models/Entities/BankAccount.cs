using System.ComponentModel.DataAnnotations.Schema;

namespace MyMiniBank.Api.Models.Entities;

public enum AccountType
{
    Savings,
    Checking,
    Business,
    Loan,
    Investment,
    FixedDeposit
}
public enum AccountStatus
{
    Active,
    Frozen,
    Closed
}
public class BankAccount
{
    public int Id { get; set; }
    public required string AccountNumber { get; set; } // Unique account number
    public AccountType AccountType { get; set; } // e.g., Savings, 
    public AccountStatus Status { get; set; } = AccountStatus.Active; // Default status is Active
    [Column(TypeName = "REAL")] // Use REAL type for decimal in SQLite
    public decimal Balance { get; set; } = 0.0m; // Default balance is 0
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Foreign key to User entity
    public int UserId { get; set; }
    public User User { get; set; } = null!; // Navigation property to User entity
}