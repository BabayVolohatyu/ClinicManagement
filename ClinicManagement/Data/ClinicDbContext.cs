using ClinicManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Data
{
    public class ClinicDbContext : DbContext
    {
        public ClinicDbContext(DbContextOptions<ClinicDbContext> options) : base(options) { }
        public DbSet<Sickness> Sicknesses {get; set; }
        public DbSet<Symptom> Symptoms {get; set; }
        public DbSet<SicknessSymptom> SicknessSymptoms {get; set; }

        public DbSet<DoctorSpecialty> DoctorSpecialties {get; set; }

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

            //DoctorSpecialty
            modelBuilder.Entity<DoctorSpecialty>()
                .HasKey(ds => ds.Id);
        }
    }
}
