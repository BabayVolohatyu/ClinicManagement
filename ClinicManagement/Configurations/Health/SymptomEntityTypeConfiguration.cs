using ClinicManagement.Data.Health;
using ClinicManagement.Models.Health;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Configurations.Health
{
    public class SymptomEntityTypeConfiguration : IEntityTypeConfiguration<Symptom>
    {
        public void Configure(EntityTypeBuilder<Symptom> builder)
        {
            builder
                .HasKey(s => s.Id);

            // SEED data
            builder.HasData(SymptomSeedData.GetSeedData());
        }
    }
}
