using ClinicManagement.Models.Humans;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Configurations.Humans
{
    public class DistrictDoctorEntityTypeConfiguration : IEntityTypeConfiguration<DistrictDoctor>
    {
        public void Configure(EntityTypeBuilder<DistrictDoctor> builder)
        {
            builder
                .HasKey(dd => dd.DoctorId);

            builder
                .HasOne(dd => dd.Doctor)
                .WithOne(d => d.DistrictDoctor)
                .HasForeignKey<DistrictDoctor>(dd => dd.DoctorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
