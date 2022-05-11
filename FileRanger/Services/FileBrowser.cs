using FileRanger.DAL.Models;
using FileRanger.DAL.Repositories;
using File = FileRanger.DAL.Models.File;

namespace FileRanger.Services;

public class FileBrowser{
    private IRepository<File> _fileRepository;
    private IRepository<Folder> _folderRepository;

    public FileBrowser(IRepository<File> fileRepository, IRepository<Folder> folderRepository) {
        _fileRepository = fileRepository;
        _folderRepository = folderRepository;
    }

    public List<Folder> GetSubFolders(string targetPath) {
        return _folderRepository.GetByCondition(x => x.ParentPath == targetPath);
    }
    
    public List<File> GetFilesForFolder(string targetPath) {
        return _fileRepository.GetByCondition(x => x.ParentPath == targetPath);
    } 

}