namespace MyMiniBank.Api.Models.DTOs
{
    /// <summary>
    /// Represents the payload of a JWT token for a user.
    /// This class contains the user's ID, username, email, and role.
    /// </summary>
    public class JwtUserPayload
    {
        public int Id { get; set; } // Unique identifier for the user, typically a GUID or string
        public string Username { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Role { get; set; } = "User";
    }
}