using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using SportPlanner.DataLayer.Data;
using SportPlanner.DataLayer.Profiles;

namespace SportPlannerApi;

public class Program
{
    public static void Main() => CreateHostBuilder(null).Build().Run();

    // EF Core uses this method at design time to access the DbContext
    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        SqlAuthenticationProvider.SetProvider(SqlAuthenticationMethod.ActiveDirectoryInteractive, new SqlAppAuthenticationProvider());

        return Host.CreateDefaultBuilder()
            .ConfigureFunctionsWorkerDefaults()
            .ConfigureServices(s =>
            {
                s.TryAddTransient<IRepository<User>, Repository<User>>();
                s.TryAddTransient<IRepository<Event>, Repository<Event>>();
                s.AddAutoMapper(typeof(SportPlannerProfile));
                s.AddDbContext<SportPlannerContext>(o => o.UseSqlServer(Environment.GetEnvironmentVariable("dbConnectionString")));
            });
    }
}