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
        var cloudTableConnectionString = Environment.GetEnvironmentVariable("cloudTableConnectionString");

        return Host.CreateDefaultBuilder()
            .ConfigureFunctionsWorkerDefaults()
            .ConfigureServices(s =>
            {
                s.AddAutoMapper(typeof(SportPlannerProfile));

                s.TryAddTransient<IEventService, EventService>();
                s.TryAddTransient<ICloudTableClient<Event>, CloudTableClient<Event>>();
                s.TryAddTransient<ICloudTableClient<EventUser>, CloudTableClient<EventUser>>();
                s.Configure<CloudTableOptions<Event>>(o =>
                {
                    o.ConnectionString = cloudTableConnectionString;
                    o.TableName = "Event";
                });
                s.Configure<CloudTableOptions<EventUser>>(o =>
                {
                    o.ConnectionString = cloudTableConnectionString;
                    o.TableName = "EventUser";
                });
            });
    }
}