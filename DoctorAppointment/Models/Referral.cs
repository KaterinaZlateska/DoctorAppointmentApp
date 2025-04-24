using System.ComponentModel.DataAnnotations;

namespace DoctorAppointment.Models
{
    public class Referral
    {
        public int ReferralId { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [Required]
        public Doctor Doctor { get; set; } = null!;

        [Required]
        public int PatientId { get; set; }

        [Required]
        public Patient Patient { get; set; } = null!;


        [MaxLength(50)]
        public string ClinicName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string ClinicAddress { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string Diagnosis { get; set; } = string.Empty;

        [Required]
        public DateTime ReferralDate { get; set; }
    }
}
