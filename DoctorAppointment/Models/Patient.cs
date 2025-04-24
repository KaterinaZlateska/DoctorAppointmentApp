using System.ComponentModel.DataAnnotations;

namespace DoctorAppointment.Models
{
    public class Patient : User
    {
        [MaxLength(1000)]
        public string? MedicalHistory { get; set; }

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<Report> Reports { get; set; } = new List<Report>();

        public ICollection<Referral> Referrals { get; set; } = new List<Referral>();
    }

}
