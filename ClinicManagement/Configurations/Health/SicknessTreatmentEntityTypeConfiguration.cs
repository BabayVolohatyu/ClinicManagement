using ClinicManagement.Models.Health;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Configurations.Health
{
    public class SicknessTreatmentEntityTypeConfiguration : IEntityTypeConfiguration<SicknessTreatment>
    {
        public void Configure(EntityTypeBuilder<SicknessTreatment> builder)
        {
            builder.
                HasKey(st => new { st.SicknessId, st.TreatmentId });

            builder
                .HasOne(st => st.Sickness)
                .WithMany(s => s.SicknessTreatment)
                .HasForeignKey(st => st.SicknessId)
                .IsRequired();

            builder
                .HasOne(st => st.Treatment)
                .WithMany(t => t.SicknessTreatment)
                .HasForeignKey(st => st.TreatmentId)
                .IsRequired();
        }
    }
}
