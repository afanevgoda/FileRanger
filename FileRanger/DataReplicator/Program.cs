using DAL.DB;
using DataReplicator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;

var hostBuilder = Host.CreateDefaultBuilder(args);
var configuration = GetConfiguration();
IHost host = hostBuilder
    .ConfigureServices(services => {
        services.AddLogging();
        services.AddHttpClient();
        var postgresConnectionString = configuration.GetSection("ConnectionString:Postgres").Value;
        Console.WriteLine($"Going connect to {postgresConnectionString}");
        services.AddDbContext<AppDbContext>(options => options
            .UseNpgsql(postgresConnectionString));
        services.AddSingleton(new ConnectionFactory { HostName = "localhost" });
        services.AddSingleton(configuration);
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();

IConfiguration GetConfiguration() {
    string confFile;
    if (args.Contains("docker"))
        confFile = "appsettings.Docker.json";
    else
        //todo: need to fix only docker-development
        confFile = "appsettings.Development.json";

    Console.WriteLine($"Going to use {confFile}");
    var configuration = new ConfigurationBuilder()
        .AddJsonFile(confFile, optional: false)
        .Build();
    return configuration;
}