using System.Runtime.CompilerServices;
using FileRanger.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FileRanger.Controllers;

public class ScannerController : Controller{
    private Scanner _scanner;
    private ILogger<ScannerController> _logger;

    public ScannerController(Scanner scanner, ILogger<ScannerController> logger) {
        _scanner = scanner;
        _logger = logger;
    }

    [HttpGet]
    public void StartScan([FromQuery] string targetDisk) {
        // await _scanner.ScanLocalDisk(targetDisk);
        var scanTask = new Task(() => _scanner.ScanLocalDisk(targetDisk));
        scanTask.Start();
    }
}