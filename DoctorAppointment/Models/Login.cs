using System.ComponentModel.DataAnnotations;

namespace DoctorAppointment.Models
{
    public class Login
    {
        [Required]
        [MaxLength(50)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Password { get; set; } = string.Empty;
    }
}
