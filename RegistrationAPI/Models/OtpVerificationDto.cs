using System.ComponentModel.DataAnnotations;

namespace RegistrationAPI.Models
{
    public class OtpVerificationDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string OTP { get; set; }
    }
}
