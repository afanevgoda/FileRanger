using Common.Snapshot.GRPC;
using DAL.Models;
using Helpers.Extensions;

namespace FileScanner.WebAppCommunication;

public class SnapshotInitializer : ISnapshotInitializer{
    private readonly IDataSender _dataSender;
    private readonly ICallouter _callouter;

    public SnapshotInitializer(IDataSender dataSender, ICallouter callouter) {
        _dataSender = dataSender;
        _callouter = callouter;
    }
    
    public async Task<int> CreateSnapshot(string targetDrive) {
        var snapshot = new AddNewSnapshot { 
            CreatedAt = DateTime.Now.ToString("O"),
            HostName = _callouter.GetHostName(),
            TargetDrive = targetDrive
        };
        var snapshotId = await _dataSender.SendNewSnapshot(snapshot);
        return snapshotId;
    }
}