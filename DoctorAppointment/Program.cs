using DoctorAppointment.Data;
using DoctorAppointment.Repositories.Interfaces;
using DoctorAppointment.Repositories;
using Microsoft.EntityFrameworkCore;
using DoctorAppointment.Services.Interfaces;
using DoctorAppointment.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();  // Add API Controllers

// Register the ApplicationDbContext for Entity Framework
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Register repositories (if you're using them)
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();


//Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();

// Add authentication and authorization services (if needed)
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();


// Add JWT Authentication from appsettings.json
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // Read the JWT settings from the configuration
        var jwtSettings = builder.Configuration.GetSection("Jwt");

        // Ensure SecretKey is not null
        var secretKey = jwtSettings["SecretKey"];
        if (string.IsNullOrEmpty(secretKey))
        {
            throw new InvalidOperationException("JWT SecretKey is missing in the configuration.");
        }

        options.TokenValidationParameters = new TokenValidationParameters
        {
            // Read the issuer, audience, and secret key from appsettings.json
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = jwtSettings["Issuer"],    // Issuer from appsettings.json
            ValidAudience = jwtSettings["Audience"],// Audience from appsettings.json
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)), // SecretKey from appsettings.json
            RoleClaimType = ClaimTypes.Role
        };
    });


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("DoctorOnly", policy => policy.RequireRole("Doctor"));
    options.AddPolicy("PatientOnly", policy => policy.RequireRole("Patient"));

});


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Authentication and Authorization Middleware (if you're using it)
app.UseAuthentication();
app.UseAuthorization();

// Map controllers to routes (including your AuthController)
app.MapControllers();  // This will ensure your controllers, including AuthController, are available

app.Run();
