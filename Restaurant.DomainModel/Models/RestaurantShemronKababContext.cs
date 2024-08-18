using Microsoft.EntityFrameworkCore;
using Restaurant.DomainModel.Models.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DomainModel.Models
{
    public class RestaurantShemronKababContext : DbContext
    {
        public RestaurantShemronKababContext(DbContextOptions<RestaurantShemronKababContext> options):base(options)
        {
            
        }
        public DbSet<Appetizer> Appetizers { get; set; }
        public DbSet<Beverages> Beverages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Appetizer>(new AppetizerConfiguration());
            modelBuilder.ApplyConfiguration<Beverages>(new BeveragesConfiguration());
            modelBuilder.ApplyConfiguration<Category>(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration<Customer>(new CustomerConfiguration());

            modelBuilder.ApplyConfiguration<Employee>(new EmployeeConfiguration());

            modelBuilder.ApplyConfiguration<Food>(new FoodConfiguration());

            modelBuilder.ApplyConfiguration<Order>(new OrderConfiguration());
            modelBuilder.ApplyConfiguration<OrderDetails>(new OrderDetailsConfiguration());

            base.OnModelCreating(modelBuilder);
        }

    }
}
