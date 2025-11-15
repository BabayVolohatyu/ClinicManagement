using ClinicManagement.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Configurations.Auth
{
    public class PromotionRequestEntityTypeConfiguration : IEntityTypeConfiguration<PromotionRequest>
    {
        public void Configure(EntityTypeBuilder<PromotionRequest> builder)
        {
            builder.HasKey(pr => pr.Id);

            builder.Property(pr => pr.Reason)
                .HasMaxLength(1000);

            builder.Property(pr => pr.RequestedAt)
                .HasColumnType("timestamptz")
                .IsRequired();

            builder.Property(pr => pr.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Property(pr => pr.ProcessedAt)
                .HasColumnType("timestamptz");

            // Relationships
            builder.HasOne(pr => pr.User)
                .WithMany()
                .HasForeignKey(pr => pr.UserId)
                .IsRequired();

            builder.HasOne(pr => pr.RequestedRole)
                .WithMany()
                .HasForeignKey(pr => pr.RequestedRoleId)
                .IsRequired();

            builder.HasOne(pr => pr.ProcessedByAdmin)
                .WithMany()
                .HasForeignKey(pr => pr.ProcessedByAdminId)
                .IsRequired(false);

            // Indexes for better query performance
            builder.HasIndex(pr => pr.Status);
            builder.HasIndex(pr => pr.RequestedAt);
            builder.HasIndex(pr => pr.UserId);

        }
    }
}