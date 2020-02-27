using System.ComponentModel.DataAnnotations;

namespace tvz2api.Models.DTO
{
    public class UserForRegisterDto 
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You must specify a password that is between 4 and 8 characters!")]
        public string Password { get; set; }
    }
}