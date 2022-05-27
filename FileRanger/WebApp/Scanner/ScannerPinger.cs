﻿namespace WebApp.Scanner;

public class ScannerPinger : BackgroundService{
    private readonly IScannerCollector _collector;

    public ScannerPinger(IScannerCollector collector) {
        _collector = collector;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
        while (!stoppingToken.IsCancellationRequested) {
            await Task.Delay(15000, stoppingToken);
            _collector.CalloutScanners();
        }
    }
}