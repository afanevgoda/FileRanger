using System.Collections.Generic;

namespace WebApp.Scanner;

public interface IScannerCollector{
    void CalloutScanners();
    void AddScanner(ScannerInfo scannerInfo);
    List<ScannerInfo> GetScanners();
    void UpdateScannersStatus();
}