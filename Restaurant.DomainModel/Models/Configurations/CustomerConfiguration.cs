using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DomainModel.Models.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(15);

            builder.Property(x => x.LastName).IsRequired().HasMaxLength(15);

            builder.Property(x => x.Age).IsRequired();

            builder.Property(x => x.Address).IsRequired().HasMaxLength(50);

            builder.Property(x => x.Mobile).IsRequired().HasMaxLength(11);

            builder.Property(x => x.TelHome).HasMaxLength(11);

            builder.Property(x => x.UserName).IsRequired().HasMaxLength(100);

            builder.Property(x => x.Password).IsRequired().HasMaxLength(50);

            builder.Property(x => x.IsActive);
        }
    }
}
