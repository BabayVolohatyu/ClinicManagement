using ClinicManagement.Models.Health;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Configurations.Health
{
    public class SicknessSymptomEntityTypeConfiguration : IEntityTypeConfiguration<SicknessSymptom>
    {
        public void Configure(EntityTypeBuilder<SicknessSymptom> builder)
        {
            builder
                .HasKey(ss => new {ss.SicknessId, ss.SymptomId});

            builder
                .HasOne(ss => ss.Sickness)
                .WithMany(s => s.SicknessSymptoms)
                .HasForeignKey(ss => ss.SicknessId)
                .IsRequired();

            builder
                .HasOne(ss => ss.Symptom)
                .WithMany(s => s.SicknessSymptoms)
                .HasForeignKey(ss => ss.SymptomId)
                .IsRequired();
        }
    }
}
