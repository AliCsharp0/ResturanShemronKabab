using DataAccess.Restaurant.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Extensions.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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
				   optionsAction.UseSqlServer(RestaurantConnectionString, options =>
				   {
					  options.EnableRetryOnFailure(
					  maxRetryCount: 5, // تعداد تلاش‌های مجدد
					  maxRetryDelay: TimeSpan.FromSeconds(30), // حداکثر تأخیر بین تلاش‌ها
					  errorNumbersToAdd: null); // شماره‌های خطاهای اضافی
				   });
			   }, ServiceLifetime.Scoped);

			services.AddScoped<ICategoryRepository, CategoryRepository>();

			services.AddScoped<IEmployeeRepository, EmployeeRepository>();

			services.AddScoped<IOrderRepository, OrderRepository>();

			services.AddScoped<IFoodRepository, FoodRepository>();

			services.AddScoped<IBeveragesRepository, BeveragesRepository>();

			services.AddScoped<IAppetizerRepository, AppetizerRepository>();

			services.AddScoped<IUserRepository, UserRepository>();

			services.AddScoped<IUserApplication, UserApplication>();

			services.AddScoped<IEmployeeApplication, EmployeeApplication>();

			services.AddScoped<ICategoryApplication, CategoryApplication>();

			services.AddScoped<IFoodApplication, FoodApplication>();

			services.AddScoped<IAppetizerApplication, AppetizerApplication>();

			services.AddScoped<IBeveragesApplication, BeveragesApplication>();

		}
	}
}
