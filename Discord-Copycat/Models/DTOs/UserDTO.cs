using System.ComponentModel.DataAnnotations;

namespace Discord_Copycat.Models.DTOs
{
    public class UserDTO
    {
        [Required]
        public String Username { get; set; } = "";

        [Required]
        public String Password { get; set; } = "";

        public string Email { get; set; } = "";
    }
}
