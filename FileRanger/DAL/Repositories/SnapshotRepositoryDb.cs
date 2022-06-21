using DAL.DB;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class SnapshotRepositoryDb : BaseRepositoryDb<Snapshot>{
    public SnapshotRepositoryDb(AppDbContext dbContext) : base(dbContext, dbContext.Snapshots) {
    }
}