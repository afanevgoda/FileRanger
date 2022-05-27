using DAL.DB;

namespace DAL.Repositories;

public class FileRepositoryDb : BaseRepositoryDb<Models.File>{
    public FileRepositoryDb(AppDbContext dbContext) : base(dbContext, dbContext.Files) {
    }
}