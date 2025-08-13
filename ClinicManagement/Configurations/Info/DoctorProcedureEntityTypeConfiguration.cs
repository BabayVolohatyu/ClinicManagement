using ClinicManagement.Models.Info;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Configurations.Info
{
    public class DoctorProcedureEntityTypeConfiguration : IEntityTypeConfiguration<DoctorProcedure>
    {
        public void Configure(EntityTypeBuilder<DoctorProcedure> builder)
        {
            throw new NotImplementedException();
        }
    }
}
