﻿using ClinicManagement.Models.Info;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Configurations.Info
{
    public class DoctorOnCallStatusEntityTypeConfiguration : IEntityTypeConfiguration<DoctorOnCallStatus>
    {
        public void Configure(EntityTypeBuilder<DoctorOnCallStatus> builder)
        {
            builder
                .HasKey(docs => docs.Id);

            builder
                .HasOne(docs => docs.Doctor)
                .WithOne(d => d.OnCallStatus)
                .HasForeignKey<DoctorOnCallStatus>(docs => docs.DoctorId)
                .IsRequired();
            builder
                .HasOne(docs => docs.Address)
                .WithMany(a => a.DoctorOnCallStatuses)
                .HasForeignKey(docs => docs.AddressId)
                .IsRequired();

            builder
                .Property(docs => docs.StartTime)
                .HasColumnType("timestamp")
                .IsRequired();

            builder
                .Property(docs => docs.EndTime)
                .HasColumnType("timestamp")
                .IsRequired();
        }
    }
}
