using DoctorAppointment.Enums;
using DoctorAppointment.Models;

namespace DoctorAppointment.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByEmailAsync(string email);
        Task<List<User>> GetUsersByRoleAsync(UserRole role);
        Task<bool> AssignRoleToUserAsync(int id, UserRole role);
        Task<bool> SetDoctorAvailabilityAsync(int id, bool isAvailable);
        Task<User> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
    }
}
