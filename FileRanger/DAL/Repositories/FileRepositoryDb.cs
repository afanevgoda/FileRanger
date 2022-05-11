using FileRanger.DAL.DB;
using File = FileRanger.DAL.Models.File;

namespace FileRanger.DAL.Repositories;

public class FileRepositoryDb : BaseRepositoryDb<File>{
    public FileRepositoryDb(AppDbContext dbContext) : base(dbContext, dbContext.Files) {
    }
}