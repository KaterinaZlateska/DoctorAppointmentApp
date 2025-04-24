using DoctorAppointment.Enums;
using DoctorAppointment.Models;

namespace DoctorAppointment.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        Task<List<User>> GetUsersByRoleAsync(UserRole role);
        Task<bool> AssignRoleAsync(int id, UserRole role);
        Task<bool> SetDoctorAvailabilityAsync(int id, bool isAvailable);
        Task AddAsync(User user);
        Task<User> UpdateAsync(User user);      
        Task<bool> DeleteAsync(int id);

    }
}
