using ClinicManagement.Models.Info;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Configurations.Info
{
    public class AppointmentEntityTypeConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder
                .HasKey(a => a.Id);

            builder
                .HasOne(a => a.DoctorProcedure)
                .WithMany(dp => dp.Appointments)
                .HasForeignKey(a => a.DoctorProcedureId)
                .IsRequired();

            builder
                .HasOne(a => a.Cabinet)
                .WithMany(c => c.Appointments)
                .HasForeignKey(a => a.CabinetId)
                .IsRequired();

            builder
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .IsRequired();
            builder
                .Property(a => a.StartTime)
                .HasColumnType("timestamptz")
                .IsRequired();

            builder
                .Property(a => a.EndTime)
                .HasColumnType("timestamptz")
                .IsRequired();
        }
    }
}
