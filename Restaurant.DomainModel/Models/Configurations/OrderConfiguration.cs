using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Restaurant.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DomainModel.Models.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.OrderDate).IsRequired();

            builder.Property(x => x.SmallDescription).HasMaxLength(100);

            builder.Property(x => x.EmployeeID).IsRequired();

            builder.Property(x => x.CustomerID).IsRequired();

            builder.Property(x => x.AddressID).IsRequired();

            builder.HasMany(x => x.orderDetails).WithOne(x => x.order).HasForeignKey(x => x.OrderID).OnDelete(DeleteBehavior.Restrict);
        }
    }
}

