using DoctorAppointment.Data;
using DoctorAppointment.Enums;
using DoctorAppointment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DoctorAppointment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(Register register)
        {
            if (await _context.Users.AnyAsync(u => u.Email == register.Email))
                return BadRequest("Email already exists.");

            User user = register.Role switch
            {
                UserRole.Doctor => new Doctor
                {
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    Email = register.Email,
                    Password = register.Password, // TODO: Hash this
                    Role = register.Role,
                    Specialization = "Default",
                    ClinicName = "Default",
                    ClinicAddress = "Default"
                },
                UserRole.Patient => new Patient
                {
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    Email = register.Email,
                    Password = register.Password,
                    Role = register.Role
                },
                UserRole.Admin => new Admin
                {
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    Email = register.Email,
                    Password = register.Password,
                    Role = register.Role
                },
                _ => throw new ArgumentException("Invalid role provided.")
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("Registration successful.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(Login dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null || user.Password != dto.Password) // TODO: Use hashed comparison
                return Unauthorized("Invalid credentials.");

            // Get the SecretKey from configuration
            var secretKey = _configuration["Jwt:SecretKey"];

            if (string.IsNullOrEmpty(secretKey))
            {
                // If the SecretKey is missing, return an error
                return StatusCode(500, "Internal server error: SecretKey is not configured.");
            }

            // Create the JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secretKey); // Get Secret Key from appsettings
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.Name, user.UserId.ToString()),
            new Claim(ClaimTypes.Role, user.Role.ToString()) // Add role as claim (0, 1, or 2)
                }),
                Expires = DateTime.UtcNow.AddHours(1), // Token expiration time (1 hour)
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);

            return Ok(new
            {
                token = jwtToken,  // Return the JWT token to the client
                user.UserId,
                user.FirstName,
                user.LastName,
                user.Role
            });
        }
    }
}
