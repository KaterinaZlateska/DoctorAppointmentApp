using DoctorAppointment.Models;
using DoctorAppointment.Repositories.Interfaces;

namespace DoctorAppointment.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        public Task AddPatientAsync(Patient patient)
        {
            throw new NotImplementedException();
        }

        public Task DeletePatientAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Patient> GetPatientByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePatientAsync(Patient patient)
        {
            throw new NotImplementedException();
        }
    }
}
