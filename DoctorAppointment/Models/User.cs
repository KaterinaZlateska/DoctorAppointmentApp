using DoctorAppointment.Enums;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace DoctorAppointment.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [MaxLength(500)]
        public string ProfilePictureUrl { get; set; } = string.Empty;

        [Required]
        public UserRole Role { get; set; }

    }
}
