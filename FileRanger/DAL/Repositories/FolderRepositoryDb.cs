using DAL.DB;
using DAL.Models;

namespace DAL.Repositories;

public class FolderRepositoryDb : BaseRepositoryDb<Folder> {
    
    public FolderRepositoryDb(AppDbContext dbContext) : base(dbContext, dbContext.Folders) {
    }
}