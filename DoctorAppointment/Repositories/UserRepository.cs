using DoctorAppointment.Data;
using DoctorAppointment.Enums;
using DoctorAppointment.Models;
using DoctorAppointment.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointment.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<List<User>> GetUsersByRoleAsync(UserRole role)
        {
            return await _context.Users.Where(u => u.Role == role).ToListAsync();
        }
        public async Task<bool> AssignRoleAsync(int id, UserRole role)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return false;

            // Change the role of the user
            user.Role = role;

            // Move user to corresponding table based on the role
            if (role == UserRole.Doctor)
            {
                // If the user was a patient, move them to the Doctors table
                var patient = await _context.Patients.FirstOrDefaultAsync(p => p.UserId == id);
                if (patient != null)
                {
                    _context.Patients.Remove(patient);  // Remove from Patients table
                }

                var doctor = new Doctor { UserId = id, FirstName = user.FirstName };  // Create a new Doctor record
                _context.Doctors.Add(doctor);  // Add to Doctors table
            }
            else if (role == UserRole.Patient)
            {
                // If the user was a doctor, move them to the Patients table
                var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.UserId == id);
                if (doctor != null)
                {
                    _context.Doctors.Remove(doctor);  // Remove from Doctors table
                }

                var patient = new Patient { UserId = id, FirstName = user.FirstName };  // Create a new Patient record
                _context.Patients.Add(patient);  // Add to Patients table
            }

            // Save changes
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> SetDoctorAvailabilityAsync(int id, bool isAvailable)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.UserId == id);
            if (doctor == null)
                return false;

            doctor.IsAvailable = isAvailable;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> UpdateAsync(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.UserId);
            if (existingUser == null)
            {
                throw new Exception("User not found");
            }

            // Update the existing tracked entity
            _context.Entry(existingUser).CurrentValues.SetValues(user);

            await _context.SaveChangesAsync();

            return existingUser;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
