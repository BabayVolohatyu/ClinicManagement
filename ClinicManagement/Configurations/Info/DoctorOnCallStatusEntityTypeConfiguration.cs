using ClinicManagement.Data.Info;
using ClinicManagement.Models.Info;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Configurations.Info
{
    public class DoctorOnCallStatusEntityTypeConfiguration : IEntityTypeConfiguration<DoctorOnCallStatus>
    {
        public void Configure(EntityTypeBuilder<DoctorOnCallStatus> builder)
        {
            builder
                .HasKey(docs => docs.Id);

            builder
                .HasOne(docs => docs.Doctor)
                .WithOne(d => d.OnCallStatus)
                .HasForeignKey<DoctorOnCallStatus>(docs => docs.DoctorId)
                .IsRequired();

            // Explicit unique index on DoctorId for one-to-one relationship
            builder
                .HasIndex(docs => docs.DoctorId)
                .IsUnique();

            builder
                .HasOne(docs => docs.Address)
                .WithMany(a => a.DoctorOnCallStatuses)
                .HasForeignKey(docs => docs.AddressId)
                .IsRequired();
            builder
                .Property(docs => docs.StartTime)
                .HasColumnType("timestamptz")
                .IsRequired();

            builder
                .Property(docs => docs.EndTime)
                .HasColumnType("timestamptz")
                .IsRequired();

            // SEED data
            builder.HasData(DoctorOnCallStatusSeedData.GetSeedData());
        }
    }
}
