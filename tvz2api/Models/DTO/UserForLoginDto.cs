using System.ComponentModel.DataAnnotations;

namespace tvz2api.Models.DTO
{
    public class UserForLoginDto 
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}