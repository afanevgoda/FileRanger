using FileRanger.DAL.Elastic.Indexers;
using File = FileRanger.DAL.Models.File;

namespace FileRanger.DAL.Repositories;

public class FileRepositoryEs : IRepository<File>{
    private IIndexer<File> _fileIndexer;
    
    public FileRepositoryEs(IIndexer<File> fileIndexer) {
        _fileIndexer = fileIndexer;
    }

    public void Add(File newEntity) => _fileIndexer.Add(newEntity);
    
    public void AddDistinctRange(List<File> newEntities) {
        throw new NotImplementedException();
    }

    public List<File> GetAll() => _fileIndexer.GetAll();
    public List<File> GetByCondition(Func<File, bool> func) {
        throw new NotImplementedException();
    }

    public void DeleteRange(List<File> entities) {
        throw new NotImplementedException();
    }
}