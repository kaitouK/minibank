using System.ComponentModel.DataAnnotations;

namespace MyMiniBank.Api.Models.DTOs
{
    /// <summary>
    /// Represents the data transfer object (DTO) for user registration requests.  
    /// </summary>
    public class RegisterRequest
    {
        [Required]
        public required string Username { get; set; }
        [Required]
        public required string Password { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public required string Email { get; set; }

        // Additional properties can be added as needed
        // For example, you might want to include a full name or phone number
        // public string? FullName { get; set; }
        // public string? PhoneNumber { get; set; }
    }


    // This class represents the data transfer object (DTO) for login responses.
    // It contains the necessary properties to be returned after a successful login.
    public class RegisterResponse
    {
        public required string Username { get; set; } // The username of the registered user
        
        // A message indicating the result of the registration

        // Additional properties can be added as needed, such as roles or permissions
    }

}  