using DoctorAppointment.Models;
using DoctorAppointment.Repositories.Interfaces;

namespace DoctorAppointment.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        public Task AddDoctorAsync(Doctor doctor)
        {
            throw new NotImplementedException();
        }

        public Task DeleteDoctorAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Doctor> GetDoctorByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateDoctorAsync(Doctor doctor)
        {
            throw new NotImplementedException();
        }
    }
}
