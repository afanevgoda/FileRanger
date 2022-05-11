using FileRanger.DAL.Models;
using Microsoft.EntityFrameworkCore;
using File = FileRanger.DAL.Models.File;

namespace FileRanger.DAL.DB;

public class AppDbContext : DbContext{
    public AppDbContext(DbContextOptions options) : base(options) {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<File>().ToTable("file")
            .Property(x => x.Id)
            .HasColumnName("rowid");
        modelBuilder.Entity<Folder>().ToTable("folder")
            .Property(x => x.Id)
            .HasColumnName("rowid");
    }

    public DbSet<Folder> Folders { get; set; }
    public DbSet<File> Files { get; set; }
}