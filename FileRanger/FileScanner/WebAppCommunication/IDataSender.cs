using Common.Snapshot;
using Common.Snapshot.GRPC;
using File = Common.Snapshot.GRPC.File;

namespace FileScanner.WebAppCommunication;

public interface IDataSender{
    Task SendFolderData(IEnumerable<Folder> newFolders);

    Task SendFilesData(IEnumerable<File> newFolders);

    Task<int> SendNewSnapshot(AddNewSnapshot snapshot);

    Task SendSnapshotResult(int snapshotId, SnapshotStatus status);
}