using System.ComponentModel.DataAnnotations;

namespace RegistrationAPI.Models
{
    public class UserLoginDto
    {
        [Required]
        public string ICNumber { get; set; }
    }
}
