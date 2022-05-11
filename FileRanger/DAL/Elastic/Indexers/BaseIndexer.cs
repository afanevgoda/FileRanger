using FileRanger.DAL.Models;
using Nest;

namespace FileRanger.DAL.Elastic.Indexers;

public class BaseIndexer<T> : IIndexer<T> where T : class{
    protected string _indexName;
    protected readonly ConnectionSettings _connectionSettings;
    
    public BaseIndexer(ConnectionSettings settings) {
        _connectionSettings = settings.DefaultIndex(_indexName);
    }

    public virtual List<T> GetAll() {
        var client = new ElasticClient(_connectionSettings);
        var searchResponse = client.Search<T>(s => s
            .From(0)
            .Size(10)
        );

        return searchResponse.Documents.ToList();
    }

    public virtual void Add(T newEntity) {
        var client = new ElasticClient(_connectionSettings);
        client.IndexDocument(newEntity);
    }
}