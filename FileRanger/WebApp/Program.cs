using System;
using DAL;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using WebApp.Scanner;

var builder = WebApplication.CreateBuilder(args);

// new StorageInitializer().Init(builder.Services);

string confFile;
if (args.Contains("docker"))
    confFile = "appsettings.Docker.json";
else
    confFile = builder.Environment.IsDevelopment() ? "appsettings.Development.json" : "appsettings.json";
Console.WriteLine($"Going to use {confFile}");
var configuration = new ConfigurationBuilder()
    .AddJsonFile(confFile, optional: false)
    .Build();
builder.Services.AddControllersWithViews();
Console.WriteLine(configuration.GetSection("SnapshotFileBrowser").Value);
builder.Services.AddSingleton(
    GrpcChannel.ForAddress(configuration.GetSection("SnapshotFileBrowser").Value)
);
builder.Services.AddScoped(x => new ConnectionFactory
    { HostName = configuration.GetSection("RmqHost").Value });
// builder.Services.AddCors();
builder.Services.AddSingleton<IScannerCollector, ScannerCollector>();
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