using System;
using System.Collections.Generic;
using System.Linq;
using DAL.DB;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class BaseRepositoryDb<T> : IRepository<T> where T : Model{
    private readonly AppDbContext _dbContext;
    private readonly DbSet<T> _targetSet;

    protected BaseRepositoryDb(AppDbContext dbContext, DbSet<T> targetSet) {
        _dbContext = dbContext;
        _targetSet = targetSet;
    }

    public T Add(T newEntity) {
        var addedEntity = _targetSet.Add(newEntity);
        _dbContext.SaveChanges();
        return addedEntity.Entity;
    }

    public void AddRange(List<T> newEntities) {
        _targetSet.AddRange(newEntities);
        _dbContext.SaveChanges();
    }

    public void AddDistinctRange(List<T> newEntities) {
        _targetSet.AddRange(newEntities.FindAll(x => !_targetSet.Contains(x)));
        _dbContext.SaveChanges();
    }

    public T? Get(int id) {
        return _targetSet.FirstOrDefault(x => x.Id == id);
    }

    public List<T> GetAll() {
        return _targetSet.ToList();
    }

    public List<T> GetByCondition(Func<T, bool> func) {
        return _targetSet.Where(func).ToList();
    }

    public void DeleteRange(List<T> entities) {
        _targetSet.RemoveRange(entities);
        _dbContext.SaveChanges();
    }

    public void Delete(T entity) {
        _targetSet.Remove(entity);
        _dbContext.SaveChanges();
    }

    public bool DoesExistWithId(int? id) {
        return !(id == null || !_dbContext.Snapshots.Any(x => x.Id == id));
    }

    public T Update(T updatedEntity) {
        var result = _targetSet.Update(updatedEntity);
        _dbContext.SaveChanges();
        return result.Entity;
    }
}