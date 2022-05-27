using System;
using DAL.DB;
using DAL.Elastic.Indexers;
using DAL.Models;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using File = DAL.Models.File;

namespace DAL;

public class StorageInitializer{
    private IConfigurationRoot _configuration;

    public StorageInitializer() {
        _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .Build();
    }

    public void Init(IServiceCollection serviceCollection) {
        serviceCollection.AddSingleton(_configuration);

        TryToInitSqlite(serviceCollection);

        // if (TryToInitElastic(serviceCollection))
        //     return;
        // if (TryToInitPostgres(serviceCollection))
        //     return;
        // if (TryToInitSqlite(serviceCollection))
        //     return;
    }

    private bool TryToInitPostgres(IServiceCollection serviceCollection) {
        if (_configuration.GetSection("Storage:Postgres:IsAllowed").Value != "True")
            return false;

        var connectionString = _configuration.GetSection("Storage:Postgres:ConnectionString").Value;
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(connectionString);
        using var dbContext = new AppDbContext(optionsBuilder.Options);

        if (!dbContext.Database.CanConnect())
            return false;

        serviceCollection
            .AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
        AddDbRepositories(serviceCollection);
        return true;
    }

    private bool TryToInitSqlite(IServiceCollection serviceCollection) {
        if (_configuration.GetSection("Storage:Sqlite:IsAllowed").Value != "True")
            return false;

        var connectionString = _configuration.GetSection("Storage:Sqlite:ConnectionString").Value;
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(connectionString);
        using var dbContext = new AppDbContext(optionsBuilder.Options);

        if (!dbContext.Database.CanConnect()) {
            CreateAndInitSqliteDB(serviceCollection);
            AddDbRepositories(serviceCollection);
            serviceCollection.AddDbContext<AppDbContext>(options => options
                .UseSqlite(connectionString));

            return true;
        }

        AddDbRepositories(serviceCollection);
        serviceCollection.AddDbContext<AppDbContext>(options => options
            .UseSqlite(connectionString));
        return true;
    }

    private bool TryToInitElastic(IServiceCollection serviceCollection) {
        if (_configuration.GetSection("Storage:Elastic:IsAllowed").Value != "True")
            return false;

        var connectionString = _configuration.GetSection("Storage:Elastic:ConnectionString").Value;

        var settings = new ConnectionSettings(new Uri(connectionString))
            .ServerCertificateValidationCallback((_, _, _, _) => true)
            .BasicAuthentication(
                _configuration.GetSection("Storage:Elastic:User").Value,
                _configuration.GetSection("Storage:Elastic:Password").Value);

        var client = new ElasticClient(settings);
        var pingResult = client.Ping();
        if (!pingResult.ApiCall.Success)
            return false;

        AddEsRepositoriesAndIndexers(serviceCollection);
        serviceCollection.AddSingleton(settings);
        return true;
    }

    private void CreateAndInitSqliteDB(IServiceCollection serviceCollection) {
        serviceCollection.AddDbContext<AppDbContext>(options => options
            .UseSqlite("Data Source=c:\\testDb\\file_ranger.db;"));
    }

    private void AddDbRepositories(IServiceCollection serviceCollection) {
        serviceCollection.AddScoped<global::DAL.Repositories.IRepository<File>, FileRepositoryDb>();
        serviceCollection.AddScoped<global::DAL.Repositories.IRepository<Folder>, FolderRepositoryDb>();
    }

    private void AddEsRepositoriesAndIndexers(IServiceCollection serviceCollection) {
        serviceCollection.AddScoped<global::DAL.Repositories.IRepository<File>, FileRepositoryEs>();
        serviceCollection.AddScoped<IIndexer<File>, FileIndexer>();
    }
}