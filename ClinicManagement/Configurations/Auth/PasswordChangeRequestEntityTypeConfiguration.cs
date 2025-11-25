using ClinicManagement.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Configurations.Auth
{
    public class PasswordChangeRequestEntityTypeConfiguration : IEntityTypeConfiguration<PasswordChangeRequest>
    {
        public void Configure(EntityTypeBuilder<PasswordChangeRequest> builder)
        {
            builder.HasKey(pcr => pcr.Id);

            builder.Property(pcr => pcr.RequestedPasswordHash)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(pcr => pcr.RequestedAt)
                .HasColumnType("timestamptz")
                .IsRequired();

            builder.Property(pcr => pcr.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Property(pcr => pcr.ProcessedAt)
                .HasColumnType("timestamptz");

            
            builder.HasOne(pcr => pcr.User)
                .WithMany()
                .HasForeignKey(pcr => pcr.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(pcr => pcr.ProcessedByAdmin)
                .WithMany()
                .HasForeignKey(pcr => pcr.ProcessedByAdminId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            
            builder.HasIndex(pcr => pcr.Status);
            builder.HasIndex(pcr => pcr.RequestedAt);
            builder.HasIndex(pcr => pcr.UserId);
        }
    }
}

