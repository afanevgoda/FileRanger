using FileRanger.DAL.DB;
using FileRanger.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FileRanger.DAL.Repositories;

public class FolderRepositoryDb : BaseRepositoryDb<Folder> {
    
    public FolderRepositoryDb(AppDbContext dbContext) : base(dbContext, dbContext.Folders) {
    }
}