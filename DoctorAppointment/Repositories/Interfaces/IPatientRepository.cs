using DoctorAppointment.Models;

namespace DoctorAppointment.Repositories.Interfaces
{
    public interface IPatientRepository
    {
        Task<Patient> GetPatientByIdAsync(int id);
        Task<IEnumerable<Patient>> GetAllPatientsAsync();
        Task AddPatientAsync(Patient patient);
        Task UpdatePatientAsync(Patient patient);
        Task DeletePatientAsync(int id);
    }
}
