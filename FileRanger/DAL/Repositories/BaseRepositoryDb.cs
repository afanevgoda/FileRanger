using System;
using System.Collections.Generic;
using System.Linq;
using DAL.DB;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class BaseRepositoryDb<T> : IRepository<T> where T : class{
    private readonly AppDbContext _dbContext;
    private readonly DbSet<T> _targetSet;

    protected BaseRepositoryDb(AppDbContext dbContext, DbSet<T> targetSet) {
        _dbContext = dbContext;
        _targetSet = targetSet;
    }

    public void Add(T newEntity) {
        _targetSet.Add(newEntity);
        _dbContext.SaveChanges();
    }

    public void AddDistinctRange(List<T> newEntities) {
        _targetSet.AddRange(newEntities.FindAll(x => !_targetSet.Contains(x)));
        _dbContext.SaveChanges();
    }

    public List<T> GetAll() {
        return _targetSet.ToList();
    }

    public List<T> GetByCondition(Func<T, bool> func) {
        return _targetSet.Where(func).ToList();
    }

    public void DeleteRange(List<T> entities) {
        _targetSet.RemoveRange(entities);
    }
}