using Grpc.Net.Client;
using RabbitMQ.Client;
using WebApp;
using WebApp.Replicator;
using WebApp.Scanner;

var builder = WebApplication.CreateBuilder(args);

var settings = BuildConfigurationSettings();
builder.Services.AddSingleton<Settings, Settings>(_ => settings);
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton(
    GrpcChannel.ForAddress(settings.SnapshotFileBrowserHost)
);
builder.Services.AddScoped(_ => new ConnectionFactory
    { HostName = settings.RmqHost });
// builder.Services.AddCors();
builder.Services.AddSingleton<IScannerCollector, ScannerCollector>();
builder.Services.AddSingleton<IReplicator, Replicator>();
builder.Services.AddHostedService<ScannerPinger>();
builder.Services.AddLogging();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// todo: configuration
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000"));
app.UseStaticFiles();
// app.UseHttpsRedirection();
// app.UseHsts();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();


Settings BuildConfigurationSettings() {
    string confFile;
    if (args.Contains("docker"))
        confFile = "appsettings.Docker.json";
    else
        confFile = builder.Environment.IsDevelopment() ? "appsettings.Development.json" : "appsettings.json";
    Console.WriteLine($"Going to use {confFile}");
    var configuration = new ConfigurationBuilder()
        .AddJsonFile(confFile, optional: false)
        .Build();
    var settings = new Settings();
    configuration.GetSection("Options").Bind(settings);
    return settings;
}