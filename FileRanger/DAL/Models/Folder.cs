using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Enum;

namespace DAL.Models;

public class Folder : Model{
    [Column("name")] public string Name { get; set; }
    [Column("fullPath")] public string FullPath { get; set; }
    [Column("parentPath")] public string ParentPath { get; set; }

    [Column("snapshotId")] public int SnapshotId { get; set; }
    [Column("status")] public ItemStatus Status { get; set; }
    [Column("size")] public float Size { get; set; }

    public Snapshot Snapshot { get; set; }
}