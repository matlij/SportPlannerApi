using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using SportPlanner.Repository.Interfaces;
using SportPlanner.Repository.Profiles;
using SportPlanner.Repository.Services;

namespace SportPlannerApi;

public class Program
{
    public static void Main() => CreateHostBuilder(null).Build().Run();

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        //var cloudTableConnectionString = Environment.GetEnvironmentVariable("cloudTableConnectionString");

        return Host.CreateDefaultBuilder()
            .ConfigureFunctionsWorkerDefaults()
            .ConfigureServices((context, services) =>
            {
                services.AddAutoMapper(typeof(SportPlannerProfile));

                services.TryAddTransient<IEventService, EventService>();
                services.TryAddTransient<ICloudTableClient<Event>, CloudTableClient<Event>>();
                services.TryAddTransient<ICloudTableClient<EventUser>, CloudTableClient<EventUser>>();

                var cloudTableConnectionString = context.Configuration.GetConnectionString("StorageTable");
                services.Configure<CloudTableOptions<Event>>(o =>
                {
                    o.ConnectionString = cloudTableConnectionString;
                    o.TableName = "Event";
                });
                services.Configure<CloudTableOptions<EventUser>>(o =>
                {
                    o.ConnectionString = cloudTableConnectionString;
                    o.TableName = "EventUser";
                });
            });
    }
}