using DoctorAppointment.Models;
using DoctorAppointment.Repositories.Interfaces;
using DoctorAppointment.Services.Interfaces;

namespace DoctorAppointment.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
        {
            return await _appointmentRepository.GetAllAppointmentsAsync();
        }

        public async Task<Appointment?> GetAppointmentByIdAsync(int id)
        {
            return await _appointmentRepository.GetAppointmentByIdAsync(id);
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByDoctorAsync(int doctorId)
        {
            return await _appointmentRepository.GetAppointmentsByDoctorAsync(doctorId);
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByPatientAsync(int patientId)
        {
            return await _appointmentRepository.GetAppointmentsByPatientAsync(patientId);
        }
        public async Task<Appointment> CreateAppointmentAsync(Appointment appointment)
        {
            return await _appointmentRepository.CreateAppointmentAsync(appointment);
        }
        public async Task<Appointment> UpdateAppointmentAsync(Appointment appointment)
        {
            return await _appointmentRepository.UpdateAppointmentAsync(appointment);
        }
        public async Task<bool> DeleteAppointmentAsync(int appointmentId)
        {
            return await _appointmentRepository.DeleteAppointmentAsync(appointmentId);
        }

    }
}
