using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DomainModel.Models.Configurations
{
    public class BeveragesConfiguration : IEntityTypeConfiguration<Beverages>
    {
        public void Configure(EntityTypeBuilder<Beverages> builder)
        {
            builder.Property(x => x.BeveragesName).IsRequired().HasMaxLength(20);

            builder.Property(x => x.UnitPrice).IsRequired();

            builder.Property(x => x.Image).IsRequired().HasMaxLength(38);

            builder.HasMany(x=>x.orderDetails).WithOne(x=>x.beverage).HasForeignKey(x=>x.BeveragesID).OnDelete(DeleteBehavior.NoAction);
        }
    }
}

