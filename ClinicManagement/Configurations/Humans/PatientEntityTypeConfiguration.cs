using ClinicManagement.Data.Humans;
using ClinicManagement.Models.Humans;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Configurations.Humans
{
    public class PatientEntityTypeConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder
                .HasKey(pt => pt.Id);

            builder
                .HasOne(pt => pt.Person)
                .WithOne(p => p.Patient)
                .HasForeignKey<Patient>(pt => pt.PersonId)
                .IsRequired();

            builder
                .HasOne(pt => pt.Address)
                .WithMany(a => a.Patients)
                .HasForeignKey(pt => pt.AddressId);

            // SEED data
            builder.HasData(PatientSeedData.GetSeedData());
        }
    }
}
