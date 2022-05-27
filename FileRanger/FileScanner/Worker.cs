using System.Text;
using FileScanner.Scanner;
using FileScanner.WebAppCommunication;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace FileScanner;

public class Worker : BackgroundService{
    private readonly ILogger<Worker> _logger;
    private ConnectionFactory _connFactory;
    private IConnection _connection;
    private IModel _channel;
    private readonly IScanner _scanner;
    private readonly ICallouter _callouter;

    public Worker(ILogger<Worker> logger, IScanner scanner, ICallouter callouter, ConnectionFactory connFactory) {
        _logger = logger;
        _scanner = scanner;
        _connFactory = connFactory;
        _callouter = callouter;
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
        SubscribeToScans();
        SubscribeToCallouts();
    }

    private void SubscribeToScans() {
        _channel.QueueDeclare(queue: "scans",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (_, ea) => {
            _logger.LogInformation("Received scan request");
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            _scanner.ScanLocalDisk(message);
        };
        _channel.BasicConsume(queue: "scans",
            autoAck: true,
            consumer: consumer);
    }

    private void SubscribeToCallouts() {
        _channel.QueueDeclare(queue: "scannersCallout",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (_, _) => {
            _logger.LogInformation("Received callout request");
            _callouter.Callout();
        };
        _channel.BasicConsume(queue: "scannersCallout",
            autoAck: true,
            consumer: consumer);
    }
}