using ClinicManagement.Models.Health;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Configurations.Health
{
    public class DiagnosisEntityTypeConfiguration : IEntityTypeConfiguration<Diagnosis>
    {
        public void Configure(EntityTypeBuilder<Diagnosis> builder)
        {
            builder
                .HasKey(d => d.Id);

            builder.
                HasOne(d => d.Appointment)
                .WithOne(a => a.Diagnosis)
                .HasForeignKey<Diagnosis>(d => d.AppointmentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
