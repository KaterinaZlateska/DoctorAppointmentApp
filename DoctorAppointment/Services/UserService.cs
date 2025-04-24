using DoctorAppointment.Enums;
using DoctorAppointment.Models;
using DoctorAppointment.Repositories.Interfaces;
using DoctorAppointment.Services.Interfaces;

namespace DoctorAppointment.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }
        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
        }
        public async Task<List<User>> GetUsersByRoleAsync(UserRole role)
        {
            return await _userRepository.GetUsersByRoleAsync(role);
        }
        public async Task<bool> AssignRoleToUserAsync(int id, UserRole role)
        {
            return await _userRepository.AssignRoleAsync(id, role);
        }

        public async Task<bool> SetDoctorAvailabilityAsync(int id, bool isAvailable)
        {
            return await _userRepository.SetDoctorAvailabilityAsync(id, isAvailable);
        }
        public async Task<User> UpdateUserAsync(User user)
        {
            return await _userRepository.UpdateAsync(user);
        }
        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _userRepository.DeleteAsync(id);
        }
    }
}
