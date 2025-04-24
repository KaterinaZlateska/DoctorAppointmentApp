using DoctorAppointment.Enums;
using System.ComponentModel.DataAnnotations;

namespace DoctorAppointment.Models
{
    public class Register
    {
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
        [MaxLength(50)]
        public string Password { get; set; } = string.Empty;

        [Required]
        public UserRole Role { get; set; } 
    }
}
