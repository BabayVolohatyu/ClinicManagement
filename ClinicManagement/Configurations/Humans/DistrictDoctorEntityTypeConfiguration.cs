using ClinicManagement.Models.Humans;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Configurations.Humans
{
    public class DistrictDoctorEntityTypeConfiguration : IEntityTypeConfiguration<DistrictDoctor>
    {
        public void Configure(EntityTypeBuilder<DistrictDoctor> builder)
        {
            throw new NotImplementedException();
        }
    }
}
