using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using SportPlanner.Repository.Interfaces;
using SportPlanner.Repository.Profiles;
using SportPlanner.Repository.Services;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((context, services) =>
    {
        services.AddAutoMapper(typeof(SportPlannerProfile));

        services.TryAddTransient<IEventService, EventService>();
        services.TryAddTransient<IUserService, UserService>();
        services.TryAddTransient<IGraphService, GraphService>();

        services.TryAddTransient<ICloudTableClient<Event>, CloudTableClient<Event>>();
        services.TryAddTransient<ICloudTableClient<EventUser>, CloudTableClient<EventUser>>();
        services.TryAddTransient<ICloudTableClient<User>, CloudTableClient<User>>();

        services.Configure<ServcicePrincipalOptions>(o =>
        {
            o.TenantId = context.Configuration["AzureTenantId"];
            o.ClientSecret = context.Configuration["AzureSportPlannerClientSecret"];
            o.ClientId = context.Configuration["AzureSportPlannerClientId"];
        });

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
        services.Configure<CloudTableOptions<User>>(o =>
        {
            o.ConnectionString = cloudTableConnectionString;
            o.TableName = "User";
        });
    })
    .Build();

host.Run();
