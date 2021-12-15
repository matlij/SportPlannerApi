using AutoMapper.Configuration;
using Microsoft.Azure.Functions.Worker.Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using SportPlanner.DataLayer;
using SportPlanner.DataLayer.Data;
using SportPlanner.DataLayer.Models;
using SportPlanner.DataLayer.Profiles;
using System;
using System.Threading.Tasks;

namespace SportPlannerApi
{
    public class Program
    {
        public static void Main()
        {
            SqlAuthenticationProvider.SetProvider(SqlAuthenticationMethod.ActiveDirectoryInteractive, new SqlAppAuthenticationProvider());

            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureServices(s =>
                {
                    s.TryAddTransient<IRepository<User>, Repository<User>>();
                    s.TryAddTransient<IRepository<Event>, Repository<Event>>();
                    s.AddAutoMapper(typeof(SportPlannerProfile));
                    s.AddDbContext<SportPlannerContext>(o => o.UseSqlServer(Environment.GetEnvironmentVariable("dbConnectionString")));
                })
                .Build();

            host.Run();
        }
    }
}