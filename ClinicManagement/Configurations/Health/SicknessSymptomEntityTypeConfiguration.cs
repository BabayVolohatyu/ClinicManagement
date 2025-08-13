using ClinicManagement.Models.Health;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Configurations.Health
{
    public class SicknessSymptomEntityTypeConfiguration : IEntityTypeConfiguration<SicknessSymptom>
    {
        public void Configure(EntityTypeBuilder<SicknessSymptom> builder)
        {
            throw new NotImplementedException();
        }
    }
}
