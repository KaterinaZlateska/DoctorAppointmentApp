using System.ComponentModel.DataAnnotations;

namespace DoctorAppointment.Models
{
    public class Report
    {
        public int ReportId { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [Required]
        public Doctor Doctor { get; set; } = null!;

        [Required]
        public int PatientId { get; set; }

        [Required]
        public Patient Patient { get; set; } = null!;

        [Required]
        public DateTime CreatedAt { get; set; } // When the report was written

        public string Diagnosis { get; set; } = string.Empty;
        public string Findings { get; set; } = string.Empty;
        public string Medications { get; set; } = string.Empty;
        public string Treatment { get; set; } = string.Empty;
        public string Therapy { get; set; } = string.Empty;
        public string Instructions { get; set; } = string.Empty;

    }
}
