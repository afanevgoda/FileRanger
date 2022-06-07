using DAL.DB;
using FileBrowser.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup();
startup.InitServices(builder);

var app = builder.Build();

app.MapGet("/", () => "GRPC only allowed");
startup.MapGrpcServices(app);
startup.MigrateDataBase(app);

app.Run();

class Startup{
    public void InitServices(WebApplicationBuilder builder) {
        builder.Services.AddGrpc();
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        // builder.Services.AddLogging();
        InitDbContext(builder);
    }

    private void InitDbContext(WebApplicationBuilder builder) {
        var confFile = builder.Environment.IsDevelopment() ? "appsettings.json" : "appsettings.Development.json";
        var configuration = new ConfigurationBuilder()
            .AddJsonFile(confFile, optional: false)
            .Build();
        Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
        Console.WriteLine(configuration.GetSection("ConnectionString").Value);
        builder.Services.AddDbContext<AppDbContext>(options => options
            .UseNpgsql(configuration.GetSection("ConnectionString").Value));
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