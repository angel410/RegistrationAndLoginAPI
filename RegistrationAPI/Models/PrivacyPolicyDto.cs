using System.ComponentModel.DataAnnotations;

namespace RegistrationAPI.Models
{
    public class PrivacyPolicyDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public bool AcceptPrivacyPolicy { get; set; }
    }
}
