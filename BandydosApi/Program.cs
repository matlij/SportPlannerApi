using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using SportPlanner.Repository.Interfaces;
using SportPlanner.Repository.Models;
using SportPlanner.Repository.Profiles;
using SportPlanner.Repository.Services;
using SportPlanner.Repository;
using Microsoft.Extensions.DependencyInjection;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((context, services) =>
    {
        services.AddAutoMapper(typeof(SportPlannerProfile));

        services.TryAddTransient<IEventService, EventService>();
        services.TryAddTransient<ICloudTableClient<Event>, CloudTableClient<Event>>();
        services.TryAddTransient<ICloudTableClient<EventUser>, CloudTableClient<EventUser>>();

        var cloudTableConnectionString = context.Configuration.GetConnectionString("StorageTableConnectionString");
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
    })
    .Build();

host.Run();