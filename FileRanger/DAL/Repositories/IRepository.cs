using System;
using System.Collections.Generic;

namespace DAL.Repositories;

public interface IRepository<T>{
    void Add(T newEntity);

    public void AddDistinctRange(List<T> newEntities);

    List<T> GetAll();
    
    List<T> GetByCondition(Func<T, bool> func);

    void DeleteRange(List<T> entities);
}