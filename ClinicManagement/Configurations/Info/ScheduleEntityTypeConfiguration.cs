using ClinicManagement.Models.Info;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Configurations.Info
{
    public class ScheduleEntityTypeConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder
                .HasKey(s => s.Id);

            builder
                .HasOne(s => s.Doctor)
                .WithMany(d => d.Schedules)
                .HasForeignKey(s => s.DoctorId)
                .IsRequired();

            builder
                .HasOne(s => s.Cabinet)
                .WithMany(c => c.Schedules)
                .HasForeignKey(s => s.CabinetId)
                .IsRequired();

            builder
                .Property(s => s.StartTime)
                .HasColumnType("timestamp")
                .IsRequired();

            builder
                .Property(s => s.EndTime)
                .HasColumnType("timestamp")
                .IsRequired();
        }
    }
}
