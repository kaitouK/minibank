using System.ComponentModel.DataAnnotations;

namespace SSR_Agile.Models
{
    public class RegisterViewModel
    {
        [Required]
        public required string Username { get; set; }
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public required string Password{ get; set; }
    }
}