using DataAccess;
using DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Helpers
{
    public static class DIModule
    {
        public static IServiceCollection RegisterModule(
           IServiceCollection services, string connectionString)
        {
            services.AddDbContext<MovieAppDbContext>(x =>
                x.UseSqlServer(connectionString));
            services.AddTransient<IRepository<MovieDto>, MovieRepository>();
            services.AddTransient<IRepository<UserDto>, UserRepository>();

            return services;
        }
    }
}
