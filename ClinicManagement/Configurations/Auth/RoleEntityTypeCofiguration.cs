using ClinicManagement.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Configurations.Auth
{
    public class RoleEntityTypeConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
                .HasMaxLength(50)
                .IsRequired();

            // Configure all boolean properties with default values
            builder.Property(r => r.CanCreate)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(r => r.CanRead)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(r => r.CanUpdate)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(r => r.CanDelete)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(r => r.CanExecuteRawQueries)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(r => r.CanAskPromotion)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(r => r.CanAcceptPromotions)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(r => r.CanViewPromotionsList)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(r => r.CanManageUsers)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(r => r.CanViewUserData)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(r => r.CanDownloadCsv)
                .IsRequired()
                .HasDefaultValue(false);

            // Index for role name
            builder.HasIndex(r => r.Name)
                .IsUnique();

        }
    }
}