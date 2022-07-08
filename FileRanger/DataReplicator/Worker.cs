using System.Text;
using DAL.DB;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace DataReplicator;

public class Worker : BackgroundService{
    private readonly ILogger<Worker> _logger;
    private ConnectionFactory _connFactory;
    private IConnection _connection;
    private IModel _channel;
    private AppDbContext _dbContext;

    public Worker(ILogger<Worker> logger, ConnectionFactory connFactory, AppDbContext context) {
        _logger = logger;
        _connFactory = connFactory;
        _dbContext = context;
        Init();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
        while (!stoppingToken.IsCancellationRequested) {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(5000, stoppingToken);
        }
    }

    private void Init() {
        _connection = _connFactory.CreateConnection();
        _channel = _connection.CreateModel();
        SubscribeToReplication();
    }
    
    private void SubscribeToReplication() {
        _channel.QueueDeclare(queue: "replicationQueue",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (_, _) => {
            _logger.LogInformation("Received replication request");
            foreach (var folder in _dbContext.Folders.Take(5)) {
                _logger.LogInformation(folder.FullPath);
            }
        };
        _channel.BasicConsume(queue: "replicationQueue",
            autoAck: true,
            consumer: consumer);
    }
}