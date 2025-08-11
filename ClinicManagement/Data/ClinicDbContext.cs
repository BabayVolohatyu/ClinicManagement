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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Sickness-Symptom many-to-many relationship
            //PrimaryKey setup
            modelBuilder.Entity<SicknessSymptom>()
                .HasKey(ss => new { ss.SicknessId, ss.SymptomId });

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
        }
    }
}
