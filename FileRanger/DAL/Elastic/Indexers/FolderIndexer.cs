using FileRanger.DAL.Models;
using Nest;

namespace FileRanger.DAL.Elastic.Indexers;

public class FolderIndexer : BaseIndexer<Folder>{
    private readonly ConnectionSettings _connectionSettings;

    public FolderIndexer(ConnectionSettings settings) : base(settings) {
    }
}