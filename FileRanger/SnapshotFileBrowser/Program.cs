using DAL.DB;
using FileBrowser.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup();
startup.InitServices(builder, args);

var app = builder.Build();

app.MapGet("/", () => "GRPC only allowed");
startup.MapGrpcServices(app);
startup.MigrateDataBase(app);

app.Run();

class Startup{
    public void InitServices(WebApplicationBuilder builder, string[] args) {
        builder.Services.AddGrpc();
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        // builder.Services.AddLogging();
        InitDbContext(builder, args);
    }

    private void InitDbContext(WebApplicationBuilder builder, string[] args) {
        string confFile;
        if (args.Contains("docker"))
            confFile = "appsettings.Docker.json";
        else
            confFile = builder.Environment.IsDevelopment() ? "appsettings.Development.json" : "appsettings.json";

        Console.WriteLine($"Going to use {confFile}");
        var configuration = new ConfigurationBuilder()
            .AddJsonFile(confFile, optional: false)
            .Build();
        
        var connectionString = configuration.GetSection("ConnectionString").Value;
        Console.WriteLine($"Going connect to {connectionString}");
        builder.Services.AddDbContext<AppDbContext>(options => options
            .UseNpgsql(connectionString));
    }

    public void MapGrpcServices(WebApplication app) {
        app.MapGrpcService<FolderServiceImpl>();
        app.MapGrpcService<SnapshotServiceImpl>();
        app.MapGrpcService<FileServiceImpl>();
    }

    public void MigrateDataBase(WebApplication app) {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.Migrate();
    }
}