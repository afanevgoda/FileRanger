namespace FileScanner.WebAppCommunication;

public interface ISnapshotInitializer{
    Task<int> CreateSnapshot(string targetDrive);
}