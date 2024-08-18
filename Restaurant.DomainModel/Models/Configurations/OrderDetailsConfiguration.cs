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
    public class OrderDetailsConfiguration : IEntityTypeConfiguration<OrderDetails>
    {
        public void Configure(EntityTypeBuilder<OrderDetails> builder)
        {
            builder.Property(x => x.OrderID).IsRequired();

            builder.Property(x => x.FoodID).IsRequired();

            builder.Property(x => x.BeveragesID).IsRequired();

            builder.Property(x => x.AppetizerID).IsRequired();
        }
    }
}
