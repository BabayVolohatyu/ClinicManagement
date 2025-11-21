using ClinicManagement.Data.Health;
using ClinicManagement.Models.Health;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Configurations.Health
{
    public class SicknessEntityTypeConfiguration : IEntityTypeConfiguration<Sickness>
    {
        public void Configure(EntityTypeBuilder<Sickness> builder)
        {
            builder
                .HasKey(s => s.Id);

            // SEED data
            builder.HasData(SicknessSeedData.GetSeedData());
        }
    }
}
