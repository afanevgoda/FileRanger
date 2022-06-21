using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Snapshot;

namespace DAL.Models;

public class Snapshot : Model{
    [Column("createdAt")] public DateTime CreatedAt { get; set; }

    [Column("drive")] public string Drive { get; set; }
    [Column("hostname")] public string Hostname { get; set; }
    [Column("result")] public SnapshotStatus Result { get; set; }
    public List<Folder> Folders { get; set; }

    public List<File> Files { get; set; }
}