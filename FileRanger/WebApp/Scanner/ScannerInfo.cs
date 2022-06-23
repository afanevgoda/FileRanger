using System;
using System.Collections.Generic;
using Common.Enum;

namespace WebApp.Scanner;

public class ScannerInfo{
    public string HostName { get; set; }
    public List<String> Drives { get; set; }
    public DateTime LastPongTime { get; set; }
    public ScannerStatus Status { get; set; }
}