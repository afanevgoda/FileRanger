using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using WebApp.Scanner;
using FileDto = Common.Snapshot.GRPC.File;
using FolderDto = Common.Snapshot.GRPC.Folder;
using FolderGrpc = Folder;
using FileGrpc = File;

namespace WebApp.Controllers;

public class ScannerController : Controller{
    private ILogger<ScannerController> _logger;
    private readonly GrpcChannel _channel;
    private readonly ConnectionFactory _conFactory;
    private readonly IScannerCollector _scannerCollector;
    private readonly IMapper _mapper;

    public ScannerController(ILogger<ScannerController> logger, IMapper mapper, GrpcChannel channel,
        ConnectionFactory conFactory,
        IScannerCollector scannerCollector) {
        _logger = logger;
        _channel = channel;
        _conFactory = conFactory;
        _scannerCollector = scannerCollector;
        _mapper = mapper;
    }

    [HttpPost]
    public void AddFilesData([FromBody] List<FileDto> newFiles) {
        var filesGrps = _mapper.Map<List<FileDto>, List<FileGrpc>>(newFiles);
        var client = new FileService.FileServiceClient(_channel);

        var message = new ListOfFiles();
        message.Files.AddRange(filesGrps);

        client.SaveFiles(message);
    }

    [HttpPost]
    public void AddFolderData([FromBody] List<FolderDto> newFolders) {
        var filesGrps = _mapper.Map<List<FolderDto>, List<FolderGrpc>>(newFolders);
        var client = new FolderService.FolderServiceClient(_channel);

        var message = new ListOfFolders();
        message.Folder.AddRange(filesGrps);

        client.SaveFoldersAsync(message);
    }

    [HttpPost]
    public void StartScan([FromQuery] string targetDisk) {
        targetDisk = targetDisk.Replace("\\", "").Replace(":", "");
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