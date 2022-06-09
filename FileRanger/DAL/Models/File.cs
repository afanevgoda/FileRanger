using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Enum;

namespace DAL.Models;

public class File{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("name")] public string Name { get; set; }
    [Column("fullPath")] public string FullPath { get; set; }
    [Column("parentPath")] public string ParentPath { get; set; }
    [Column("extension")] public string Extension { get; set; }
    [Column("snapshotId")] public int SnapshotId { get; set; }
    [Column("status")] public ItemStatus Status { get; set; }

    public Snapshot Snapshot { get; set; }
}