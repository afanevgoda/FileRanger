using System.Collections.Generic;
using System.Linq;
using Nest;

namespace DAL.Elastic.Indexers;

public class BaseIndexer<T> : IIndexer<T> where T : class{
    protected string IndexName = null!;
    private readonly ConnectionSettings _connectionSettings;
    
    protected BaseIndexer(ConnectionSettings settings) {
        _connectionSettings = settings.DefaultIndex(IndexName);
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