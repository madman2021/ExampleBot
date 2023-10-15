using System.Reflection;
using GhostRunnerStaffProfile;
using GhostRunnerStaffProfile.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureHostConfiguration(config =>
    {
        config.Sources.Clear();
        config.AddJsonFile("config.json",true);
        config.AddEnvironmentVariables();
        config.AddUserSecrets(Assembly.GetExecutingAssembly());
    }).ConfigureLogging(l=>
    {
        l.ClearProviders();
        l.AddConsole();
    })
    .ConfigureServices(services =>
    {
        services.AddBotServices();
        services.AddLogging();
    })
    .Build();

using (var scope = host.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<Db>();
    db.Database.EnsureCreated();
    db.Database.Migrate();
}
await host.RunAsync();
    