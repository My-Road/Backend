﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyRoad.Domain.Employees;

namespace MyRoad.Infrastructure.Employees
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.FullName).HasMaxLength(50).HasColumnType("nvarchar").IsRequired();
            builder.Property(x => x.JobTitle).HasMaxLength(30).HasColumnType("nvarchar").IsRequired();
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.EndDate).IsRequired(false);
            builder.Property(x => x.PhoneNumber).HasColumnType("nvarchar").HasMaxLength(15).IsRequired();
            builder.Property(x => x.Address).HasColumnType("nvarchar").HasMaxLength(50);
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.TotalDueAmount).HasColumnType("decimal(10,2)").IsRequired();
            builder.Property(x => x.TotalPaidAmount).HasColumnType("decimal(10,2)").IsRequired();
            builder.HasIndex(x => x.PhoneNumber).IsUnique();
            builder.Ignore(x => x.RemainingAmount);
            builder.ToTable("Employee");
        }
    }
}