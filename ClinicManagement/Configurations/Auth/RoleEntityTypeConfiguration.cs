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

            builder.Property(r => r.Type)
                .IsRequired();

            
            builder.Property(r => r.CanCreate).HasDefaultValue(false).IsRequired();
            builder.Property(r => r.CanRead).HasDefaultValue(false).IsRequired();
            builder.Property(r => r.CanUpdate).HasDefaultValue(false).IsRequired();
            builder.Property(r => r.CanDelete).HasDefaultValue(false).IsRequired();
            builder.Property(r => r.CanExecuteRawQueries).HasDefaultValue(false).IsRequired();
            builder.Property(r => r.CanAskPromotion).HasDefaultValue(false).IsRequired();
            builder.Property(r => r.CanAcceptPromotions).HasDefaultValue(false).IsRequired();
            builder.Property(r => r.CanViewPromotionsList).HasDefaultValue(false).IsRequired();
            builder.Property(r => r.CanManageUsers).HasDefaultValue(false).IsRequired();
            builder.Property(r => r.CanViewUserData).HasDefaultValue(false).IsRequired();
            builder.Property(r => r.CanDownloadCsv).HasDefaultValue(false).IsRequired();

            builder.HasMany(r => r.Users)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(r => r.Name)
                .IsUnique();

            
            builder.HasData(
                new Role
                {
                    Id = 1,
                    Name = "Guest",
                    Type = RoleType.Guest,
                    CanCreate = false,
                    CanRead = true,
                    CanUpdate = false,
                    CanDelete = false,
                    CanExecuteRawQueries = false,
                    CanAskPromotion = false,
                    CanAcceptPromotions = false,
                    CanViewPromotionsList = false,
                    CanManageUsers = false,
                    CanViewUserData = false,
                    CanDownloadCsv = false
                },
                new Role
                {
                    Id = 2,
                    Name = "Authorized",
                    Type = RoleType.Authorized,
                    CanCreate = true,
                    CanRead = true,
                    CanUpdate = true,
                    CanDelete = true,
                    CanExecuteRawQueries = false,
                    CanAskPromotion = true,
                    CanAcceptPromotions = false,
                    CanViewPromotionsList = false,
                    CanManageUsers = false,
                    CanViewUserData = false,
                    CanDownloadCsv = false
                },
                new Role
                {
                    Id = 3,
                    Name = "Operator",
                    Type = RoleType.Operator,
                    CanCreate = true,
                    CanRead = true,
                    CanUpdate = true,
                    CanDelete = true,
                    CanExecuteRawQueries = true,
                    CanAskPromotion = true,
                    CanAcceptPromotions = false,
                    CanViewPromotionsList = false,
                    CanManageUsers = false,
                    CanViewUserData = false,
                    CanDownloadCsv = true
                },
                new Role
                {
                    Id = 4,
                    Name = "Admin",
                    Type = RoleType.Admin,
                    CanCreate = true,
                    CanRead = true,
                    CanUpdate = true,
                    CanDelete = true,
                    CanExecuteRawQueries = true,
                    CanAskPromotion = false,
                    CanAcceptPromotions = true,
                    CanViewPromotionsList = true,
                    CanManageUsers = true,
                    CanViewUserData = true,
                    CanDownloadCsv = true
                }
                );

        }
    }
}

