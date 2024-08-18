using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DomainModel.Models.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(20);

            builder.Property(x => x.LastName).IsRequired().HasMaxLength(20);

            builder.Property(x => x.Address).IsRequired().HasMaxLength(100);

            builder.Property(x => x.Mobile).IsRequired().HasMaxLength(11);

            builder.Property(x => x.TelHome).HasMaxLength(11);

            builder.Property(x => x.Age).IsRequired();

            builder.Property(x => x.DateRecruitment).IsRequired();

            builder.Property(x => x.UserName).IsRequired().HasMaxLength(25);

            builder.Property(x => x.Password).IsRequired().HasMaxLength(100);
		}
	}
}
