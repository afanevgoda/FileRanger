using DAL.Models;
using Microsoft.EntityFrameworkCore;
using File = DAL.Models.File;

namespace DAL.DB;

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
        modelBuilder.Entity<Snapshot>().ToTable("snapshot")
            .Property(x => x.Id)
            .HasColumnName("rowid");
        modelBuilder.Entity<Folder>()
            .HasOne(x => x.Snapshot)
            .WithMany(x => x.Folders)
            .HasForeignKey(x => x.SnapshotId);
        modelBuilder.Entity<File>()
            .HasOne(x => x.Snapshot)
            .WithMany(x => x.Files)
            .HasForeignKey(x => x.SnapshotId);
    }

    public DbSet<Folder> Folders { get; set; }
    public DbSet<File> Files { get; set; }

    public DbSet<Snapshot> Snapshots { get; set; }
}