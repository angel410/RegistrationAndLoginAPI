using System.ComponentModel.DataAnnotations;

namespace RegistrationAPI.Models
{
    public class CreatePinDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Pin { get; set; }
    }
}
