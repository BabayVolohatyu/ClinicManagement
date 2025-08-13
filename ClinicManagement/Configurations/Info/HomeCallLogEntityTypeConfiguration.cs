using ClinicManagement.Models.Info;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Configurations.Info
{
    public class HomeCallLogEntityTypeConfiguration : IEntityTypeConfiguration<HomeCallLog>
    {
        public void Configure(EntityTypeBuilder<HomeCallLog> builder)
        {
            throw new NotImplementedException();
        }
    }
}
