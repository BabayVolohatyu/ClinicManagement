using ClinicManagement.Data.Health;
using ClinicManagement.Models.Health;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Configurations.Health
{
    public class SicknessProcedureEntityTypeConfiguration : IEntityTypeConfiguration<SicknessProcedure>
    {
        public void Configure(EntityTypeBuilder<SicknessProcedure> builder)
        {
            builder
                .HasKey(sp => new { sp.SicknessId, sp.ProcedureId });

            builder
                .HasOne(sp => sp.Sickness)
                .WithMany(s => s.SicknessProcedures)
                .HasForeignKey(sp => sp.SicknessId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(sp => sp.Procedure)
                .WithMany(p => p.SicknessProcedures)
                .HasForeignKey(sp => sp.ProcedureId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // SEED data
            builder.HasData(SicknessProcedureSeedData.GetSeedData());
        }
    }
}
