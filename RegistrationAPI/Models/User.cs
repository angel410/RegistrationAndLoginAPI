using System;
using System.ComponentModel.DataAnnotations;

namespace RegistrationAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string ICNumber { get; set; }
        [Required]
        public string MobileNumber { get; set; }
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        public string OTP { get; set; }
        public bool IsVerified { get; set; }
        public bool HasAcceptedPrivacyPolicy { get; set; }
        public string Pin { get; set; }
        public bool IsPinConfirmed { get; set; }
    }
}

