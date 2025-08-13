using ClinicManagement.Models.Health;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Configurations.Health
{
    public class TreatmentEntityTypeConfiguration : IEntityTypeConfiguration<Treatment>
    {
        public void Configure(EntityTypeBuilder<Treatment> builder)
        {
            throw new NotImplementedException();
        }
    }
}
