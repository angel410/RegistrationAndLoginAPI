using System.ComponentModel.DataAnnotations;

namespace RegistrationAPI.Models
{
    public class ConfirmPinDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string ConfirmPin { get; set; }
    }
}
