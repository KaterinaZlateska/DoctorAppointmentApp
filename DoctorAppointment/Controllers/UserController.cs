using DoctorAppointment.Enums;
using DoctorAppointment.Models;
using DoctorAppointment.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        //[AllowAnonymous]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        // [AllowAnonymous]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound("User not found.");

            return Ok(user);
        }

        [HttpGet("email/{email}")]
        [Authorize(Roles = "Admin")]
        // [AllowAnonymous]

        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _userService.GetUserByEmailAsync(email);
            if (user == null)
                return NotFound("User not found.");

            return Ok(user);
        }

        // Get all users by a specific role (doctor, patient, admin)
        [HttpGet("role/{role}")]
        [Authorize(Roles = "Admin")]
        // [AllowAnonymous]
        public async Task<IActionResult> GetUsersByRole(UserRole role)
        {
            var users = await _userService.GetUsersByRoleAsync(role);
            return Ok(users);
        }


        // Assign a role (doctor, patient, admin) to a user
        [HttpPost("{id}/role/{role}")]
        [Authorize(Roles = "Admin")]
        // [AllowAnonymous]
        public async Task<IActionResult> AssignRoleToUser(int id, UserRole role)
        {
            if (!Enum.IsDefined(typeof(UserRole), role)) // Check if the role is valid
                return BadRequest("Invalid role.");

            var success = await _userService.AssignRoleToUserAsync(id, role);
            if (!success)
                return BadRequest("Failed to assign role.");

            return Ok("Role assigned successfully.");
        }

        // Set a doctor's availability (true/false)
        [HttpPut("{id}/availability")]
        [Authorize(Roles = "Admin")]
        // [AllowAnonymous]
        public async Task<IActionResult> SetDoctorAvailability(int id, [FromQuery] bool isAvailable)
        {
            var success = await _userService.SetDoctorAvailabilityAsync(id, isAvailable);
            if (!success)
                return BadRequest("Failed to set availability.");

            return Ok($"Doctor availability set to {isAvailable}.");
        }

        [HttpPut("{id}")]
        [Authorize]
        //  [AllowAnonymous]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            // Ensure the user is updating their own profile (or an admin is updating someone else's profile)
            if (id != user.UserId)
            {
                return Unauthorized("You can only update your own profile.");
            }

            // Check if the user exists
            var existingUser = await _userService.GetUserByIdAsync(id);
            if (existingUser == null)
                return NotFound($"User with ID {id} not found.");

            // Prevent changing sensitive information like user ID, role, and email
            user.UserId = existingUser.UserId;  // Ensure the user can't change their ID
            user.Role = existingUser.Role;      // Ensure the user can't change their role
            user.Email = existingUser.Email;    // Prevent email change

            // Update the user (only the allowed fields like first name, last name, etc.)
            var updatedUser = await _userService.UpdateUserAsync(user);
            if (updatedUser == null)
                return StatusCode(500, "An error occurred while updating the user.");

            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        //  [AllowAnonymous]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var success = await _userService.DeleteUserAsync(id);
            if (!success)
                return NotFound("User not found to delete.");

            return Ok("User deleted successfully.");
        }
    }
}

