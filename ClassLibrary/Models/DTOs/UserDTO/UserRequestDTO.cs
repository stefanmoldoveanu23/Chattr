using System.ComponentModel.DataAnnotations;

namespace ClassLibrary.Models.DTOs.UserDTO
{
    public class UserRequestDTO
    {
        [Required]
        public string Username { get; set; } = "";

        [Required]
        public string Password { get; set; } = "";

        public string Email { get; set; } = "";
    }
}
