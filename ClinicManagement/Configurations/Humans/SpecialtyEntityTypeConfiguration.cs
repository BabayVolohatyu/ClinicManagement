using ClinicManagement.Models.Humans;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Configurations.Humans
{
    public class SpecialtyEntityTypeConfiguration : IEntityTypeConfiguration<Specialty>
    {
        public void Configure(EntityTypeBuilder<Specialty> builder)
        {
            builder
                .HasKey(s =>  s.Id);
        }
    }
}
