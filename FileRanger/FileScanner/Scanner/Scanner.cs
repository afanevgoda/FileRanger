using DtoLibrary.Snapshot.GRPC;
using FileScanner.WebAppCommunication;
using File = DtoLibrary.Snapshot.GRPC.File;

namespace FileScanner.Scanner;

public class Scanner : IScanner{
    private IServiceProvider _serviceProvider;
    private ILogger<Scanner> _logger;
    private readonly IDataSender _dataSender;
    private readonly ISnapshotInitializer _snapshotInitializer;
    private int _snapshotId;

    private List<Folder> _foldersToAdd = new();
    private List<File> _filesToAdd = new();

    public Scanner(IServiceProvider serviceProvider, ILogger<Scanner> logger, IDataSender dataSender, ISnapshotInitializer snapshotInitializer) {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _dataSender = dataSender;
        _snapshotInitializer = snapshotInitializer;
    }

    public async Task ScanLocalDisk(string targetDrive) {
        try {
            _snapshotId = await _snapshotInitializer.CreateSnapshot(targetDrive);
            await ScanDirectory($"{targetDrive}:\\");
            AddBatchIntoStorage(true);
            _logger.LogInformation($"Scanning disk {targetDrive} is finished");
        }
        catch (Exception ex) {
            _logger.LogError(ex, $"Couldn't scan disk {targetDrive}");
        }
    }

    private async Task ScanDirectory(string targetDirectory) {
        try {
            var directory = new DirectoryInfo(targetDirectory);
            foreach (var subDirectory in directory.GetDirectories()) {
                _foldersToAdd.Add(new Folder {
                    Name = subDirectory.Name,
                    FullPath = subDirectory.FullName,
                    ParentPath = targetDirectory,
                    SnapshotId = _snapshotId
                });
                await ScanDirectory(subDirectory.FullName);
            }

            ScanFiles(directory);
            AddBatchIntoStorage();
        }
        catch (UnauthorizedAccessException) {
            _logger.LogError($"Could not access {targetDirectory}");
        }
        catch (Exception ex) {
            _logger.LogError(ex, $"Couldn't scan directory {targetDirectory}");
        }

        _logger.LogInformation($"Scanned {_foldersToAdd.Count} folders");
    }

    private void ScanFiles(DirectoryInfo targetDirectory) {
        foreach (var file in targetDirectory.GetFiles()) {
            _filesToAdd.Add(new File {
                Name = file.Name,
                FullPath = file.FullName,
                Extension = file.Extension,
                ParentPath = targetDirectory.FullName,
                SnapshotId = _snapshotId
            });
        }
    }

    private readonly object _folderLock = new();
    private readonly object _fileLock = new();

    private void AddBatchIntoStorage(bool addAnyway = false) {
        if (_foldersToAdd.Count >= 1000 || addAnyway) {
            lock (_folderLock) {
                _dataSender.SendFolderData(_foldersToAdd);
                _foldersToAdd.Clear();
            }
        }

        if (_filesToAdd.Count >= 1000 || addAnyway) {
            lock (_fileLock) {
                _dataSender.SendFilesData(_filesToAdd);
                _filesToAdd.Clear();
            }
        }
    }
}

public interface IScanner{
    public Task ScanLocalDisk(string targetDisk);
}