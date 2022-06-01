using System.Collections.Concurrent;
using Common.Snapshot;
using Common.Snapshot.GRPC;
using FileScanner.WebAppCommunication;
using File = Common.Snapshot.GRPC.File;

namespace FileScanner.Scanner;

public class Scanner : IScanner{
    private ILogger<Scanner> _logger;
    private readonly IDataSender _dataSender;
    private readonly ISnapshotInitializer _snapshotInitializer;
    private int _snapshotId;

    private List<Folder> _foldersToAdd = new();
    private List<File> _filesToAdd = new();

    public Scanner(ILogger<Scanner> logger, IDataSender dataSender,
        ISnapshotInitializer snapshotInitializer) {
        _logger = logger;
        _dataSender = dataSender;
        _snapshotInitializer = snapshotInitializer;
    }

    public async Task ScanLocalDisk(string targetDrive) {
        try {
            _snapshotId = await _snapshotInitializer.CreateSnapshot(targetDrive);
            await ScanDirectory($"{targetDrive}:\\");
            AddFoldersIntoStorage(true);
            AddFilesIntoStorage(true);
            await _dataSender.SendSnapshotResult(_snapshotId, SnapshotStatus.Success);
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
                AddFoldersIntoStorage();
            }

            ScanFiles(directory);
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

        AddFilesIntoStorage();
    }

    private object filesLock = new ();
    private object foldersLock = new ();

    private async void AddFoldersIntoStorage(bool addAnyway = false) {
        if (_foldersToAdd.Count >= 1000 || addAnyway) {
            lock (foldersLock) {
                _dataSender.SendFolderData(_foldersToAdd);
                _foldersToAdd.Clear();
            }
        }
    }

    private async void AddFilesIntoStorage(bool addAnyway = false) {
        if (_filesToAdd.Count >= 1000 || addAnyway) {
            lock (filesLock) {
                _dataSender.SendFilesData(_filesToAdd);
                _filesToAdd.Clear();
            }
        }
    }
}

public interface IScanner{
    public Task ScanLocalDisk(string targetDisk);
}