using DAL.Models;
using Nest;

namespace DAL.Elastic.Indexers;

public class FolderIndexer : BaseIndexer<Folder>{
    public FolderIndexer(ConnectionSettings settings) : base(settings) {
    }
}