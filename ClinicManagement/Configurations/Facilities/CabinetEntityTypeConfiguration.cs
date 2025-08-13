using ClinicManagement.Models.Facilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Configurations.Facilities
{
    public class CabinetEntityTypeConfiguration : IEntityTypeConfiguration<Cabinet>
    {
        public void Configure(EntityTypeBuilder<Cabinet> builder)
        {
            throw new NotImplementedException();
        }
    }
}
