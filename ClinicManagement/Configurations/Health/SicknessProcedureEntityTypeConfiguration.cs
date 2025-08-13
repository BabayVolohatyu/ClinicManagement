using ClinicManagement.Models.Health;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Configurations.Health
{
    public class SicknessProcedureEntityTypeConfiguration : IEntityTypeConfiguration<SicknessProcedure>
    {
        public void Configure(EntityTypeBuilder<SicknessProcedure> builder)
        {
            throw new NotImplementedException();
        }
    }
}
