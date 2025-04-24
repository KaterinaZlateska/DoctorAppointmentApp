using DoctorAppointment.Enums;
using System.ComponentModel.DataAnnotations;

namespace DoctorAppointment.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int? PatientId { get; set; }
        public Patient? Patient { get; set; }

        public int? DoctorId { get; set; }
        public Doctor? Doctor { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public DateTime AppointmentTime { get; set; }

        [Required]
        public AppointmentStatus Status { get; set; }
    }
}
