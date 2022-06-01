using DAL;
using Grpc.Net.Client;
using RabbitMQ.Client;
using WebApp.Scanner;

var builder = WebApplication.CreateBuilder(args);

// new StorageInitializer().Init(builder.Services);

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton(
    GrpcChannel.ForAddress(builder.Configuration.GetSection("SnapshotFileBrowser").Value)
);
builder.Services.AddScoped(x => new ConnectionFactory
    { HostName = builder.Configuration.GetSection("RmqHost").Value });
builder.Services.AddCors();
builder.Services.AddSingleton<IScannerCollector, ScannerCollector>();
builder.Services.AddHostedService<ScannerPinger>();
builder.Services.AddLogging();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000"));
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();