using Microsoft.EntityFrameworkCore;
using RegistrationAPI.Models;

namespace RegistrationAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<ConfirmPinDto> ConfirmPinDto { get; set; }
        public DbSet<CreatePinDto> CreatePinDto { get; set; }
        public DbSet<OtpVerificationDto> OtpVerificationDto { get; set; }
        public DbSet<PrivacyPolicyDto> PrivacyPolicyDto { get; set; }
        public DbSet<UserLoginDto> UserLoginDto { get; set; }
        public DbSet<UserRegistrationDto> UserRegistrationDto { get; set; }
    }
}
