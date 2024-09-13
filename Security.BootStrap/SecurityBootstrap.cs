
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Security.Application.Implementations;
using Security.ApplicationServiceContract.Services;
using Security.DataAccess;
using Security.DataAccessServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Security.BootStrap
{
    public static class SecurityBootstrap
    {
        public static void WireUp(IServiceCollection services,string SecurityRestaurantConnectionString)
        {
            services.AddDbContext<Security.Domain.SecurityContext>(optionsAction =>
            {
                optionsAction.UseSqlServer(SecurityRestaurantConnectionString);
            }, ServiceLifetime.Scoped);
            services.AddScoped<IAcountRepository, AccountRepository>();
            services.AddScoped<IAccountApplication, AccountBuss>();
            services.AddScoped<IAuthHelper, AuthHelper>();
            services.AddScoped<Framework.Services.IPasswordHasher, Framework.PasswordHasher>();
        }
    }
}