using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoctorAppointment.Models
{
    public class Doctor : User
    {
        [Required]
        [MaxLength(100)]
        public string Specialization { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string ClinicName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string ClinicAddress { get; set; } = string.Empty;

        public bool IsAvailable { get; set; } = true;

        public ICollection<Availability> Availabilities { get; set; } = new List<Availability>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

        public ICollection<Report> Reports { get; set; } = new List<Report>();
        public ICollection<Referral> Referrals { get; set; } = new List<Referral>();
    }
}
