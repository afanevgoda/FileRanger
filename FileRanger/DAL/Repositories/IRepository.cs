using System;
using System.Collections.Generic;

namespace DAL.Repositories;

public interface  IRepository<T>{
    T Add(T newEntity);

    void AddRange(List<T> newEntities);

    public void AddDistinctRange(List<T> newEntities);

    T? Get(int id);
    
    List<T> GetAll();

    List<T> GetByCondition(Func<T, bool> func);

    void DeleteRange(List<T> entities);
    void Delete(T entity);

    bool DoesExistWithId(int? id);

    T Update(T updatedEntity);
}