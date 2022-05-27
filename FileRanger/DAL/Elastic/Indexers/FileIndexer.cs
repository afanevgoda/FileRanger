using Nest;

namespace DAL.Elastic.Indexers;

public class FileIndexer : BaseIndexer<Models.File>{
    public FileIndexer(ConnectionSettings settings) : base(settings) {
        IndexName = "file";
    }
}