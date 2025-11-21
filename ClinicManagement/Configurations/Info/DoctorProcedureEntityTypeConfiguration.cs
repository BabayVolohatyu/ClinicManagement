using ClinicManagement.Data.Info;
using ClinicManagement.Models.Info;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Configurations.Info
{
    public class DoctorProcedureEntityTypeConfiguration : IEntityTypeConfiguration<DoctorProcedure>
    {
        public void Configure(EntityTypeBuilder<DoctorProcedure> builder)
        {
            builder
                .HasKey(dp => dp.Id);

            builder
                .HasOne(dp => dp.Doctor)
                .WithMany(d => d.DoctorProcedures)
                .HasForeignKey(d => d.DoctorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(dp => dp.Procedure)
                .WithMany(p => p.DoctorProcedures)
                .HasForeignKey(d => d.ProcedureId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // SEED data
            builder.HasData(DoctorProcedureSeedData.GetSeedData());
        }
    }
}
