using ClinicManagement.Models.Facilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Configurations.Facilities
{
    public class CabinetTypeEntityTypeConfiguration : IEntityTypeConfiguration<CabinetType>
    {
        public void Configure(EntityTypeBuilder<CabinetType> builder)
        {
            builder
                .HasKey(ct => ct.Id);
        }
    }
}
