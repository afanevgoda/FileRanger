using System.Collections.Generic;

namespace DAL.Elastic.Indexers;

public interface IIndexer<IModel>{
    public List<IModel> GetAll();

    public void Add(IModel newEntity);
}