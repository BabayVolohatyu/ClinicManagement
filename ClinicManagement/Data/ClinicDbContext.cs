using ClinicManagement.Models;
using ClinicManagement.Models.Doctor;
using ClinicManagement.Models.Sickness;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Data
{
    public class ClinicDbContext : DbContext
    {
        public ClinicDbContext(DbContextOptions<ClinicDbContext> options) : base(options) { }
        public DbSet<Sickness> Sicknesses {get; set; }
        public DbSet<Symptom> Symptoms {get; set; }
        public DbSet<SicknessSymptom> SicknessSymptoms {get; set; }
        public DbSet<Treatment> Treatments {get; set; }
        public DbSet<SicknessTreatment> SicknessTreatments {get; set; }
        public DbSet<Doctor> Doctors {get; set; }
        public DbSet<Specialty> Specialties {get; set; }
        public DbSet<Address> Addresses {get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Sickness
            modelBuilder.Entity<Sickness>()
                .HasKey(s => s.Id);

            //Symptom
            modelBuilder.Entity<Symptom>()
                .HasKey(s => s.Id);

            //SicknessSymptom
            modelBuilder.Entity<SicknessSymptom>()
                .HasKey(ss => new { ss.SicknessId, ss.SymptomId });

            //Sickness-Symptom many-to-many relationship
            //One to many from SS to Sickness  
            modelBuilder.Entity<SicknessSymptom>()
                .HasOne(ss => ss.Sickness)
                .WithMany(s => s.SicknessSymptoms)
                .HasForeignKey(ss => ss.SicknessId);

            //One to many from SS to Symptom
            modelBuilder.Entity<SicknessSymptom>()
                .HasOne(ss => ss.Symptom)
                .WithMany(s => s.SicknessSymptoms)
                .HasForeignKey(ss => ss.SymptomId);

            //Treatment
            modelBuilder.Entity<Treatment>()
                .HasKey(t => t.Id);

            //SicknessTreatment
            modelBuilder.Entity<SicknessTreatment>()
                .HasKey(st => new { st.SicknessId, st.TreatmentId });

            //Sickness-Treatment many-to-many relationship
            //One-to-many from ST to Sickness
            modelBuilder.Entity<SicknessTreatment>()
                .HasOne(st => st.Sickness)
                .WithMany(t => t.SicknessTreatment)
                .HasForeignKey(st => st.SicknessId);

            //One-to-many from ST to Treatment
            modelBuilder.Entity<SicknessTreatment>()
                .HasOne(st => st.Treatment)
                .WithMany(s => s.SicknessTreatment)
                .HasForeignKey(st => st.TreatmentId);

            //Doctor
            modelBuilder.Entity<Doctor>()
                .HasKey(d => d.Id);

            //Specialty
            modelBuilder.Entity<Specialty>()
                .HasKey(ds => ds.Id);

            //Doctor - Specialty many-to-one relationship
            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Specialty)
                .WithMany(s => s.Doctors)
                .HasForeignKey(d => d.SpecialtyId)
                .IsRequired();

            //Address 
            modelBuilder.Entity<Address>()
                .HasKey(a => a.Id);
        }
    }
}
