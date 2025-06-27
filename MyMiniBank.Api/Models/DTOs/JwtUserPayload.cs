namespace MyMiniBank.Api.Models.DTOs
{
    public class JwtUserPayload
    {
        public int Id { get; set; }
        public string Username { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Role { get; set; } = "User";
    }
}