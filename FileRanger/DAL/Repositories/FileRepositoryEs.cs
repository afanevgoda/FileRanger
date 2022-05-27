using System;
using System.Collections.Generic;
using DAL.Elastic.Indexers;
using File = DAL.Models.File;

namespace DAL.Repositories;

public class FileRepositoryEs : IRepository<Models.File>{
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