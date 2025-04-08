using System;
using System.Collections.Generic;
using System.Linq;


using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyRoad.Infrastructure.Wokrers
{
    public class WorkersConfiguration : IEntityTypeConfiguration<Workers>
    {
        public void Configure(EntityTypeBuilder<Workers> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.FullName).HasMaxLength(50).HasColumnType("nvarchar").IsRequired();
            builder.Property(x => x.JobTitle).HasMaxLength(30).HasColumnType("nvarchar").IsRequired();
            builder.Property(x => x.DailySalary).HasColumnType("decimal(5,2)").IsRequired();
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.PhoneNumber).HasColumnType("nvarchar").HasMaxLength(10);
            builder.Property(x => x.Address).HasColumnType("nvarchar").HasMaxLength(50);
            builder.Property(x => x.Status).HasConversion(x => x.ToString(), x => (WorkerStatus)Enum.Parse(typeof(WorkerStatus), x)).IsRequired();
            builder.Property(x => x.Notes).HasColumnType("nvarchar").HasMaxLength(500);
            builder.Property(x => x.TotalDebt).HasColumnType("decimal(5,2)").IsRequired();
            builder.Property(x => x.TotalPaid).HasColumnType("decimal(5,2)").IsRequired();

            builder.ToTable("Workers");




        }
    }
}
