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
    public class AppetizerConfiguration : IEntityTypeConfiguration<Appetizer>
    {
        public void Configure(EntityTypeBuilder<Appetizer> builder)
        {
            builder.Property(x => x.AppetizerName).IsRequired().HasMaxLength(30);

            builder.Property(x => x.UnitPrice).IsRequired();

            builder.Property(x => x.SmallDescription).IsRequired().HasMaxLength(200);

            builder.Property(x => x.ImageURL).IsRequired().HasMaxLength(300);

            builder.HasMany(x=>x.orderDetails).WithOne(x=>x.appetizer).HasForeignKey(x=>x.AppetizerID).OnDelete(DeleteBehavior.NoAction);
        }
    }
}

