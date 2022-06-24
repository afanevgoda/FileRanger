using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace WebApp.Scanner;

public class ScannerPinger : BackgroundService{
    private readonly IScannerCollector _collector;
    private readonly Settings _settings;

    public ScannerPinger(IScannerCollector collector, Settings settings) {
        _collector = collector;
        _settings = settings;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
        while (!stoppingToken.IsCancellationRequested) {
            _collector.CalloutScanners();
            _collector.UpdateScannersStatus();
            await Task.Delay(_settings.ScannerCalloutIntervalInSeconds * 1000, stoppingToken);
        }
    }
}