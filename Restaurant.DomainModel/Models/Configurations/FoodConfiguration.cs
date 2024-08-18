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
    public class FoodConfiguration : IEntityTypeConfiguration<Food>
    {
        public void Configure(EntityTypeBuilder<Food> builder)
        {
            builder.Property(x => x.FoodName).IsRequired().HasMaxLength(30);
            
            builder.Property(x=>x.Materials).IsRequired().HasMaxLength(200);

            builder.HasMany(x=>x.orderDetails).WithOne(x=>x.food).HasForeignKey(x=>x.FoodID).OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.Image).HasMaxLength(50);

            builder.Property(x => x.UnitPrice).IsRequired();
        }
    }
}