using ClinicManagement.Models.Info;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Configurations.Info
{
    public class HomeCallLogEntityTypeConfiguration : IEntityTypeConfiguration<HomeCallLog>
    {
        public void Configure(EntityTypeBuilder<HomeCallLog> builder)
        {
            builder
                .HasKey(hcl => hcl.Id);

            builder
                .HasOne(hcl => hcl.Doctor)
                .WithMany(d => d.HomeCallLogs)
                .HasForeignKey(hcl => hcl.DoctorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(hcl => hcl.Address)
                .WithMany(a => a.HomeCallLogs)
                .HasForeignKey(hcl => hcl.AddressId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(hcl => hcl.DateTime)
                .HasColumnType("timestamptz")
                .IsRequired();
        }
    }
}
