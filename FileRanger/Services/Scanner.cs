using System.Diagnostics;
using FileRanger.Controllers;
using FileRanger.DAL.Models;
using FileRanger.DAL.Repositories;
using File = FileRanger.DAL.Models.File;

namespace FileRanger.Services;

public class Scanner{
    private IRepository<File> _fileRepository;
    private IRepository<Folder> _folderRepository;
    private IServiceProvider _serviceProvider;
    private ILogger<Scanner> _logger;

    private List<Folder> _foldersToAdd = new();
    private List<File> _filesToAdd = new();

    public Scanner(IServiceProvider serviceProvider, ILogger<Scanner> logger) {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task ScanLocalDisk(string targetDisk) {
        using var scope = _serviceProvider.CreateScope();
        try {
            InitRepositories(scope);
            ClearAllData();

            var timer = new Stopwatch();
            timer.Start();
            await ScanDirectory($"{targetDisk}:\\");
            AddBatchIntoStorage(true);
            timer.Stop();
            _logger.LogInformation($"Scanning disk {targetDisk} is finished. Took {timer.Elapsed.TotalSeconds}s");
        }
        catch (Exception ex) {
            _logger.LogError(ex, $"Couldn't scan disk {targetDisk}");
        }
    }

    private async Task ScanDirectory(string targetDirectory) {
        try {
            var directory = new DirectoryInfo(targetDirectory);
            foreach (var subDirectory in directory.GetDirectories()) {
                _foldersToAdd.Add(new Folder {
                    Name = subDirectory.Name,
                    FullPath = subDirectory.FullName,
                    ParentPath = targetDirectory
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
                ParentPath = targetDirectory.FullName
            });
        }
    }

    private readonly object _folderLock = new();
    private readonly object _fileLock = new();

    private void AddBatchIntoStorage(bool addAnyway = false) {
        if (_foldersToAdd.Count >= 1000 || addAnyway) {
            lock (_folderLock) {
                _folderRepository.AddDistinctRange(_foldersToAdd);
                _foldersToAdd.Clear();
            }
        }

        if (_filesToAdd.Count >= 1000 || addAnyway) {
            lock (_fileLock) {
                _fileRepository.AddDistinctRange(_filesToAdd);
                _filesToAdd.Clear();
            }
        }
    }

    private void InitRepositories(IServiceScope scope) {
        _folderRepository = scope.ServiceProvider.GetRequiredService<IRepository<Folder>>();
        _fileRepository = scope.ServiceProvider.GetRequiredService<IRepository<File>>();
    }

    private void ClearAllData() {
        var allFolders = _folderRepository.GetAll();
        _folderRepository.DeleteRange(allFolders);
        var allFiles = _fileRepository.GetAll();
        _fileRepository.DeleteRange(allFiles);
    }
}