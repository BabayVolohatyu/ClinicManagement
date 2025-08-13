using ClinicManagement.Models.Health;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Configurations.Health
{
    public class SicknessTreatmentEntityTypeConfiguration : IEntityTypeConfiguration<SicknessTreatment>
    {
        public void Configure(EntityTypeBuilder<SicknessTreatment> builder)
        {
            throw new NotImplementedException();
        }
    }
}
