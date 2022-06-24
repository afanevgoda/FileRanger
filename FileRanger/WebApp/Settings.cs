using Microsoft.Extensions.Options;

namespace WebApp;

public class Settings{
    public string SnapshotFileBrowserHost { get; set; }
    public string RmqHost { get; set; }
    public int ScannerCalloutIntervalInSeconds { get; set; }
}