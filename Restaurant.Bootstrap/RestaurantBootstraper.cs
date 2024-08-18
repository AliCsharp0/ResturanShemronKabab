using DataAccess.Restaurant.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Extensions.Internal;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Application;
using Restaurant.ApplicationServiceContract.Services;
using Restaurant.DataAccessServiceContract.Repositories;
using Restaurant.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Bootstrap
{
    public static class RestaurantBootstraper
    {
        public static void WireUp(IServiceCollection services, string RestaurantConnectionString)
        {
            services.AddDbContext<RestaurantShemronKababContext>
               (optionsAction =>
               {
                   optionsAction.UseSqlServer(RestaurantConnectionString);
               }, ServiceLifetime.Scoped);

            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped<IFoodRepository, FoodRepository>();

            services.AddScoped<IBeveragesRepository, BeveragesRepository>();

            services.AddScoped<IAppetizerRepository, AppetizerRepository>();

            services.AddScoped<IEmployeeApplication,EmployeeApplication >();

            services.AddScoped<ICategoryApplication , CategoryApplication >();

            services.AddScoped<ICustomerApplication, CustomerApplication>();

            services.AddScoped<IFoodApplication, FoodApplication>();

            services.AddScoped<IAppetizerApplication, AppetizerApplication>();

            services.AddScoped<IBeveragesApplication, BeveragesApplication>();

		}
    }
}
