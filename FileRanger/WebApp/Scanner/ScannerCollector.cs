using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grpc.Net.Client;
using RabbitMQ.Client;

namespace WebApp.Scanner;

public class ScannerCollector : IScannerCollector{
    private readonly GrpcChannel _channel;
    private readonly ConnectionFactory _conFactory;
    private List<ScannerInfo> Scanners = new ();

    public ScannerCollector(GrpcChannel channel, ConnectionFactory conFactory) {
        _channel = channel;
        _conFactory = conFactory;
    }

    public void CalloutScanners() {
        using var connection = _conFactory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.QueueDeclare(queue: "scannersCallout",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        
        var body = Encoding.UTF8.GetBytes("");

        channel.BasicPublish(exchange: "",
            routingKey: "scannersCallout",
            basicProperties: null,
            body: body);
    }

    public void AddScanner(ScannerInfo scannerInfo) {
        var sameScanner = Scanners.FirstOrDefault(x => x.HostName == scannerInfo.HostName);
        var isAlreadyAdded = sameScanner != null;

        if (isAlreadyAdded)
            return;
        
        Scanners.Add(scannerInfo);
    }

    public List<ScannerInfo> GetScanners() => Scanners;
}