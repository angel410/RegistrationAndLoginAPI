using System.ComponentModel.DataAnnotations;

namespace RegistrationAPI.Models
{
    public class UserRegistrationDto
    {
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string ICNumber { get; set; }
        [Required]
        public string MobileNumber { get; set; }
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
    }
}

