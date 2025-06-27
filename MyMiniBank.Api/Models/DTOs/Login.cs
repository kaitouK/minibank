using System.ComponentModel.DataAnnotations;

namespace MyMiniBank.Api.Models.DTOs
{
    // This class represents the data transfer object (DTO) for login requests.
    // It contains the necessary properties for a user to log in.
    // The 'required' keyword indicates that these properties must be provided when creating an instance of this class.
    // Additional properties can be added as needed, such as a remember me option.
    public class LoginRequest
    {
        [Required]
        public required string Username { get; set; }
        [Required]
        [DataType(DataType.Password)] // Ensures that the password is treated as a secure string
        public required string Password { get; set; }

        // Additional properties can be added as needed
        // For example, you might want to include a remember me option
        // public bool RememberMe { get; set; } = false;
    }
    /// <summary>
    /// This class represents the data transfer object (DTO) for login responses.
    /// It contains the necessary properties to be returned after a successful login.
    /// </summary>
    public class LoginResponse
    {
        public required string Token { get; set; } // The authentication token for the user
        public required string Username { get; set; } // The username of the logged-in user
        public required DateTime ExpiresAt { get; set; } // The expiration time of the token

        // Additional properties can be added as needed, such as roles or permissions
    }
}