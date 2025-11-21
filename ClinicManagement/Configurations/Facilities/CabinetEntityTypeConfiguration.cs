using ClinicManagement.Data.Facilities;
using ClinicManagement.Models.Facilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Configurations.Facilities
{
    public class CabinetEntityTypeConfiguration : IEntityTypeConfiguration<Cabinet>
    {
        public void Configure(EntityTypeBuilder<Cabinet> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder
                .HasOne(c => c.Type)
                .WithMany(ct => ct.Cabinets)
                .HasForeignKey(c => c.TypeId)
                .IsRequired();

            // SEED data
            builder.HasData(CabinetSeedData.GetSeedData());
        }
    }
}
