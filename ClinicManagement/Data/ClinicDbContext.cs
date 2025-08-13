using ClinicManagement.Models.Facilities;
using ClinicManagement.Models.Humans;
using ClinicManagement.Models.Health;
using Microsoft.EntityFrameworkCore;
using ClinicManagement.Models.Info;


namespace ClinicManagement.Data
{
    public class ClinicDbContext : DbContext
    {
        public ClinicDbContext(DbContextOptions<ClinicDbContext> options) : base(options) { }
        
        //Simple models(first wave)
        public DbSet<Person> People { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<CabinetType> CabinetTypes { get; set; }
        public DbSet<Symptom> Symptoms { get; set; }
        public DbSet<Sickness> Sicknesses { get; set; }
        public DbSet<Treatment> Treatments { get; set; }
        public DbSet<Procedure> Procedures { get; set; }

        //Models that depend only on previous ones(second wave)
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Cabinet> Cabinets { get; set; }
        public DbSet<SicknessSymptom> SicknessSymptoms { get; set; }
        public DbSet<SicknessTreatment> SicknessTreatments { get; set; }
        public DbSet<SicknessProcedure> SicknessProcedures { get; set; }

        //Models that depend only on previous ones(third wave)
        public DbSet<DistrictDoctor> DistrictDoctors { get; set; }
        public DbSet<DoctorOnCallStatus> DoctorOnCallStatuses { get; set; }
        public DbSet<DoctorProcedure> DoctorProcedures { get; set; }
        public DbSet<Schedule> Schedules { get; set; }

        //Models that depend only on previous ones(fourth wave)
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<HomeCallLog> HomeCallLogs { get; set; }

        //Models that depend only on previous ones(fifth wave)
        public DbSet<Diagnosis> Diagnoses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
