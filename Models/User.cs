using System.ComponentModel.DataAnnotations;

namespace SSR_Agile.Models;

public class User
{
    public int Id { get; set; }

    public required string Username { get; set; }
    public required string PasswordHash { get; set; }
    public required string Email { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}