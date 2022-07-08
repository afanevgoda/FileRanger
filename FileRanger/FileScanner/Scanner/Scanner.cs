using System.Collections.Concurrent;
using Common.Enum;
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
    private readonly IConfiguration _configuration;

    // private List<Folder> _foldersToAdd = new();
    private ConcurrentBag<Folder> _foldersToAdd = new();
    private ConcurrentBag<File> _filesToAdd = new();
    private readonly int _filesLimitToSend;
    private readonly int _foldersLimitToSend;

    private bool _cancellationBool;

    public Scanner(ILogger<Scanner> logger, IDataSender dataSender,
        ISnapshotInitializer snapshotInitializer, IConfiguration configuration) {
        _logger = logger;
        _dataSender = dataSender;
        _snapshotInitializer = snapshotInitializer;
        _configuration = configuration;
        Int32.TryParse(_configuration.GetSection("filesLimitToSend").Value, out _filesLimitToSend);
        Int32.TryParse(_configuration.GetSection("foldersLimitToSend").Value, out _foldersLimitToSend);
    }

    public async Task ScanLocalDisk(string targetDrive) {
        try {
            _snapshotId = await _snapshotInitializer.CreateSnapshot(targetDrive);
            await ScanDirectory($"{targetDrive}:\\");
            await AddFoldersIntoStorage(true);
            await AddFilesIntoStorage(true);
            await _dataSender.SendSnapshotResult(_snapshotId,
                _cancellationBool ? SnapshotStatus.Fail : SnapshotStatus.Success);
            _logger.LogInformation($"Scanning disk {targetDrive} is finished");
            if (_cancellationBool)
                _logger.LogInformation($"Scan was cancelled");
        }
        catch (Exception ex) {
            _logger.LogError(ex, $"Couldn't scan disk {targetDrive}");
            await _dataSender.SendSnapshotResult(_snapshotId, SnapshotStatus.Fail);
        }
    }

    private async Task<float> ScanDirectory(string targetDirectory) {
        var totalFilesSize = 0f;
        try {
            var directory = new DirectoryInfo(targetDirectory);
            foreach (var subDirectory in directory.GetDirectories()) {
                if (_cancellationBool)
                    break;
                try {
                    var filesSize = await ScanFiles(subDirectory);
                    var filesSizeInSubdirectories = await ScanDirectory(subDirectory.FullName);
                    totalFilesSize += filesSize + filesSizeInSubdirectories;
                    _foldersToAdd.Add(new Folder {
                        Name = subDirectory.Name,
                        FullPath = subDirectory.FullName,
                        ParentPath = targetDirectory,
                        SnapshotId = _snapshotId,
                        Status = ItemStatus.Ok,
                        Size = filesSize
                    });
                    await AddFoldersIntoStorage();
                }
                catch (UnauthorizedAccessException ex) {
                    _foldersToAdd.Add(new Folder {
                        Name = subDirectory.Name,
                        FullPath = subDirectory.FullName,
                        ParentPath = targetDirectory,
                        SnapshotId = _snapshotId,
                        Status = ItemStatus.Failed
                    });
                    _logger.LogError($"Could not access {targetDirectory}");
                }
            }
        }
        catch (UnauthorizedAccessException ex) {
            _foldersToAdd.Add(new Folder {
                Name = targetDirectory,
                FullPath = targetDirectory,
                ParentPath = targetDirectory,
                SnapshotId = _snapshotId,
                Status = ItemStatus.Failed
            });
            _logger.LogError($"Could not access {targetDirectory}");
        }
        catch (Exception ex) {
            _foldersToAdd.Add(new Folder {
                Name = targetDirectory,
                FullPath = targetDirectory,
                ParentPath = targetDirectory,
                SnapshotId = _snapshotId,
                Status = ItemStatus.Failed
            });
            _logger.LogError(ex, $"Couldn't scan directory {targetDirectory}");
        }

        _logger.LogInformation($"Scanned {_foldersToAdd.Count} folders");
        return totalFilesSize;
    }

    private async Task<float> ScanFiles(DirectoryInfo targetDirectory) {
        var temp = new List<File>();
        foreach (var file in targetDirectory.GetFiles()) {
            if (_cancellationBool)
                break;
            var fileToAdd = new File {
                Name = file.Name,
                FullPath = file.FullName,
                Extension = file.Extension,
                ParentPath = targetDirectory.FullName,
                SnapshotId = _snapshotId,
                Size = file.Length / 1000f
            };
            _filesToAdd.Add(fileToAdd);
            temp.Add(fileToAdd);
            await AddFilesIntoStorage();
        }

        return temp.Sum(x => x.Size);
    }

    private object _foldersLock = new();

    private async Task AddFoldersIntoStorage(bool addAnyway = false) {
        if (_foldersToAdd.Count >= _foldersLimitToSend || addAnyway) {
            try {
                List<Folder> foldersTemp;
                lock (_foldersLock) {
                    foldersTemp = new List<Folder>(_foldersToAdd);
                    _foldersToAdd.Clear();
                }

                await _dataSender.SendFolderData(foldersTemp);
            }
            catch (Exception e) {
                _logger.LogError(e, "Going to cancel scan");
                _cancellationBool = true;
            }
        }
    }

    private object _filesLock = new object();

    private async Task AddFilesIntoStorage(bool addAnyway = false) {
        if (_filesToAdd.Count >= _filesLimitToSend || addAnyway) {
            try {
                List<File> filesTemp;
                lock (_filesLock) {
                    filesTemp = new List<File>(_filesToAdd);
                    _filesToAdd.Clear();
                }

                await _dataSender.SendFilesData(filesTemp);
            }
            catch (Exception e) {
                _logger.LogError(e, "Going to cancel scan");
                _cancellationBool = true;
            }
        }
    }
}

public interface IScanner{
    public Task ScanLocalDisk(string targetDisk);
}