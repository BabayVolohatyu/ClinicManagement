using ClinicManagement.Data.Health;
using ClinicManagement.Models.Health;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Configurations.Health
{
    public class ProcedureEntityTypeConfiguration : IEntityTypeConfiguration<Procedure>
    {
        public void Configure(EntityTypeBuilder<Procedure> builder)
        {
            builder
                .HasKey(p => p.Id);

            // SEED data
            builder.HasData(ProcedureSeedData.GetSeedData());
        }
    }
}
