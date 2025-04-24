using System.ComponentModel.DataAnnotations;

namespace DoctorAppointment.Models
{
    public class Availability
    {
        [Required]
        public int AvailabilityId { get; set; }
        [Required]
        public int DoctorId { get; set; }

        [Required]
        public Doctor Doctor { get; set; } = null!;

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        [Required]
        public bool IsAvailable { get; set; } = true;

    }
}
