namespace MyMiniBank.Api.Models.Entities 
{
    public class User
    {
        public int Id { get; set; } // Unique identifier for the user
        public required string Username { get; set; } // Unique username for the user
        public required string PasswordHash { get; set; } 
        public required string Email { get; set; }
        public string Role { get; set; } = "User"; // Default role is User, can be changed to Admin or others
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<BankAccount> BankAccounts { get; set; } = new List<BankAccount>();

        // Additional properties can be added as needed
    }
}