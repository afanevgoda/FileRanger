using FileScanner;
using FileScanner.Scanner;
using FileScanner.WebAppCommunication;
using RabbitMQ.Client;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services => {
        services.AddLogging();
        services.AddHttpClient();

        services.AddSingleton(new ConnectionFactory { HostName = "localhost" });
        services.AddHostedService<Worker>();
        services.AddScoped<IDataSender, DataSender>();
        services.AddScoped<IScanner, Scanner>();
        services.AddScoped<ICallouter, Callouter>();
        services.AddScoped<ISnapshotInitializer, SnapshotInitializer>();
    })
    .Build();

await host.RunAsync();