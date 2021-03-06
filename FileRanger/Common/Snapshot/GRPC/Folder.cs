using Common.Enum;

namespace Common.Snapshot.GRPC;

public class Folder{
    public int Id { get; set; }
    public string Name { get; set; }
    public string FullPath { get; set; }
    public string ParentPath { get; set; }
    public int SnapshotId { get; set; }
    public ItemStatus Status { get; set; }
    public float Size { get; set; }
}