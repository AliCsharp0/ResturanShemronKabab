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
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.CategoryName).IsRequired().HasMaxLength(50);

            builder.Property(x => x.SmallDescription).HasMaxLength(300);

            builder.HasMany(x=>x.Foods).WithOne(x=>x.category).HasForeignKey(x=>x.CategoryID).OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.beverages).WithOne(x => x.category).HasForeignKey(x => x.CategoryID).OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.appetizers).WithOne(x => x.category).HasForeignKey(x => x.CategoryID).OnDelete(DeleteBehavior.Restrict);

			builder.Property(x => x.Slug).IsRequired().HasMaxLength(100);

			builder.HasMany(x => x.Children).WithOne(x => x.Parent) .HasForeignKey(x => x.ParentID) .OnDelete(DeleteBehavior.NoAction);
		}
    }
}
