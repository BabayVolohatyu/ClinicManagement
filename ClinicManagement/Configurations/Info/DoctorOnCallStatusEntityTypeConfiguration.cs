using ClinicManagement.Models.Info;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Configurations.Info
{
    public class DoctorOnCallStatusEntityTypeConfiguration : IEntityTypeConfiguration<DoctorOnCallStatus>
    {
        public void Configure(EntityTypeBuilder<DoctorOnCallStatus> builder)
        {
            throw new NotImplementedException();
        }
    }
}
