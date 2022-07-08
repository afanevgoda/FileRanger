using System.Text;
using Common.Enum;
using RabbitMQ.Client;

namespace WebApp.Scanner;

public class ScannerCollector : IScannerCollector{
    private readonly Settings _settings;
    private readonly ConnectionFactory _conFactory;
    private readonly List<ScannerInfo> _scanners = new();

    public ScannerCollector(Settings settings, ConnectionFactory conFactory) {
        _settings = settings;
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

    private object _addScannerLock = new();

    public void AddScanner(ScannerInfo scannerInfo) {
        lock (_addScannerLock) {
            var sameScanner = _scanners.FirstOrDefault(x => x.HostName == scannerInfo.HostName);
            if (sameScanner != null) {
                var index = _scanners.IndexOf(sameScanner);
                _scanners[index].Status = ScannerStatus.Connected;
                _scanners[index].LastPongTime = DateTime.Now;
            }
            else {
                scannerInfo.Status = ScannerStatus.Connected;
                scannerInfo.LastPongTime = DateTime.Now;
                _scanners.Add(scannerInfo);
            }
        }
    }

    public List<ScannerInfo> GetScanners() => _scanners;

    public void UpdateScannersStatus() {
        _scanners.ForEach(x => {
            if ((DateTime.Now - x.LastPongTime).TotalSeconds > _settings.ScannerCalloutIntervalInSeconds * 8)
                x.Status = ScannerStatus.Disconnected;
            else if ((DateTime.Now - x.LastPongTime).TotalSeconds >
                     _settings.ScannerCalloutIntervalInSeconds * 4)
                x.Status = ScannerStatus.WaitingToConnect;
            else
                x.Status = ScannerStatus.Connected;
        });
    }
}