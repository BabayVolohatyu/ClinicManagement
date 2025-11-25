using ClinicManagement.Data.Auth;
using ClinicManagement.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Configurations.Auth
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.MiddleName)
                .HasMaxLength(100);

            builder.Property(u => u.LastName)
                .HasMaxLength(100);

            builder.Property(u => u.Email)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(u => u.PasswordHash)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(u => u.CreatedAt)
                .HasColumnType("timestamptz")
                .IsRequired();

            
            builder.HasIndex(u => u.Email)
                .IsUnique();

            
            builder.HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .IsRequired();

            
            builder.HasData(AuthSeedData.GetSeedData());
        }
    }
}