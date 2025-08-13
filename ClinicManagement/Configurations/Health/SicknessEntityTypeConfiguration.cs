using ClinicManagement.Models.Health;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Configurations.Health
{
    public class SicknessEntityTypeConfiguration : IEntityTypeConfiguration<Sickness>
    {
        public void Configure(EntityTypeBuilder<Sickness> builder)
        {
            throw new NotImplementedException();
        }
    }
}
