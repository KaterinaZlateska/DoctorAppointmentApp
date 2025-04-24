using DoctorAppointment.Enums;
using DoctorAppointment.Models;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointment.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets
        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Referral> Referrals { get; set; }
        public DbSet<Availability> Availabilities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // This enables Table-Per-Type (TPT) mapping
            modelBuilder.Entity<User>().ToTable("Users"); // Ова кажува дека User е базна класа


            modelBuilder.Entity<Patient>().ToTable("Patients")
           .HasOne<User>() 
           .WithOne() 
           .HasForeignKey<Patient>(p => p.UserId) 
           .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<Doctor>().ToTable("Doctors")
                .HasOne<User>() 
                .WithOne() 
                .HasForeignKey<Doctor>(d => d.UserId) 
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<Admin>().ToTable("Admins")
                .HasOne<User>() 
                .WithOne() 
                .HasForeignKey<Admin>(a => a.UserId) 
                .OnDelete(DeleteBehavior.Cascade); 

            // Configure relationships and constraints if needed
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Report>()
                .HasOne(r => r.Doctor)
                .WithMany(d => d.Reports)
                .HasForeignKey(r => r.DoctorId)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<Report>()
                .HasOne(r => r.Patient)
                .WithMany(p => p.Reports)
                .HasForeignKey(r => r.PatientId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Referral>()
                .HasOne(r => r.Doctor)
                .WithMany(d => d.Referrals)
                .HasForeignKey(r => r.DoctorId)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<Referral>()
                .HasOne(r => r.Patient)
                .WithMany(p => p.Referrals)
                .HasForeignKey(r => r.PatientId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Availability>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Availabilities)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Cascade); 
        }


    }
}
