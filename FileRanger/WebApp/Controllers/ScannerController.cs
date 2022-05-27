using System.Text;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using WebApp.Scanner;

namespace WebApp.Controllers;

public class ScannerController : Controller{
    private ILogger<ScannerController> _logger;
    private readonly IConfiguration _configuration;
    private readonly GrpcChannel _channel;
    private readonly ConnectionFactory _conFactory;
    private readonly IScannerCollector _scannerCollector;

    public ScannerController(ILogger<ScannerController> logger, IConfiguration configuration, GrpcChannel channel, ConnectionFactory conFactory,
        IScannerCollector scannerCollector) {
        _logger = logger;
        _configuration = configuration;
        _channel = channel;
        _conFactory = conFactory;
        _scannerCollector = scannerCollector;
    }
    
    [HttpPost]
    public void AddFilesData([FromBody] IEnumerable<File> newFiles) {
        var client = new FileService.FileServiceClient(_channel);

        var message = new ListOfFiles();
        message.Files.AddRange(newFiles);

        client.SaveFiles(message);
    }

    [HttpPost]
    public void AddFolderData([FromBody] IEnumerable<Folder> newFolders) {
        var client = new FolderService.FolderServiceClient(_channel);

        var message = new ListOfFolders();
        message.Folder.AddRange(newFolders);

        client.SaveFoldersAsync(message);
    }
    
    [HttpGet]
    public void StartScan([FromQuery] string targetDisk) {
        using var connection = _conFactory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.QueueDeclare(queue: "scans",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var message = targetDisk;
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: "",
            routingKey: "scans",
            basicProperties: null,
            body: body);
        Console.WriteLine(" [x] Sent {0}", message);
    }

    [HttpPost]
    public void Callout([FromBody] ScannerInfo scannerInfo) {
        _scannerCollector.AddScanner(scannerInfo);
    }
    
    [HttpGet]
    public List<ScannerInfo> GetAvailableScanners() {
        return _scannerCollector.GetScanners();
    }
}