using FileRanger.DAL.Models;
using Nest;
using File = FileRanger.DAL.Models.File;

namespace FileRanger.DAL.Elastic.Indexers;

public class FileIndexer : BaseIndexer<File>{
    private readonly ConnectionSettings _connectionSettings;
    private string _indexName = "file";

    public FileIndexer(ConnectionSettings settings) : base(settings) {
    }
}