using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistrationAPI.Data;
using RegistrationAPI.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto registrationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the user with the same IC number already exists
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.ICNumber == registrationDto.ICNumber);

            if (existingUser != null)
            {
                return Conflict(new { message = "Account with this IC number already exists." });
            }

            var user = new User
            {
                CustomerName = registrationDto.CustomerName,
                ICNumber = registrationDto.ICNumber,
                MobileNumber = registrationDto.MobileNumber,
                EmailAddress = registrationDto.EmailAddress,
                OTP = GenerateOtp(),
                IsVerified = false,
                HasAcceptedPrivacyPolicy = false,
                IsPinConfirmed = false
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            SendOtpSms(user.MobileNumber, user.OTP);

            return Ok(new { userId = user.Id, message = "User registered successfully. OTP sent to mobile." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.ICNumber == loginDto.ICNumber);

            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            // Generate a new OTP and send it to the user's mobile number
            user.OTP = GenerateOtp();
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            SendOtpSms(user.MobileNumber, user.OTP);

            return Ok(new { userId = user.Id, message = "OTP sent to mobile." });
        }

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] OtpVerificationDto otpVerificationDto)
        {
            var user = await _context.Users.FindAsync(otpVerificationDto.UserId);

            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            if (user.OTP == otpVerificationDto.OTP)
            {
                user.IsVerified = true;
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(new { message = "User verified successfully." });
            }

            return BadRequest(new { message = "Invalid OTP." });
        }

        [HttpPost("accept-privacy-policy")]
        public async Task<IActionResult> AcceptPrivacyPolicy([FromBody] PrivacyPolicyDto privacyPolicyDto)
        {
            var user = await _context.Users.FindAsync(privacyPolicyDto.UserId);

            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            user.HasAcceptedPrivacyPolicy = privacyPolicyDto.AcceptPrivacyPolicy;
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Privacy policy accepted." });
        }

        [HttpPost("create-pin")]
        public async Task<IActionResult> CreatePin([FromBody] CreatePinDto createPinDto)
        {
            var user = await _context.Users.FindAsync(createPinDto.UserId);

            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            user.Pin = createPinDto.Pin;
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new { message = "PIN created." });
        }

        [HttpPost("confirm-pin")]
        public async Task<IActionResult> ConfirmPin([FromBody] ConfirmPinDto confirmPinDto)
        {
            var user = await _context.Users.FindAsync(confirmPinDto.UserId);

            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            if (user.Pin == confirmPinDto.ConfirmPin)
            {
                user.IsPinConfirmed = true;
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(new { message = "PIN confirmed. User can now enable fingerprint authentication." });
            }

            return BadRequest(new { message = "PIN confirmation failed. PINs do not match." });
        }

        private string GenerateOtp()
        {
            var random = new Random();
            //ensures that the number is formatted as a 4-digit string
            return random.Next(1000, 9999).ToString("D4");
        }

        private void SendOtpSms(string mobileNumber, string otp)
        {
            // Simulate sending OTP SMS (In production, use a third-party SMS service)
            Console.WriteLine($"Sending OTP {otp} to {mobileNumber}");
        }
    }
}
