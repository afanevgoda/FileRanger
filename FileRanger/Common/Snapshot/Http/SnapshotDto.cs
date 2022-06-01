namespace Common.Snapshot.Http;

public class SnapshotDto{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Drive { get; set; }
    public string Hostname { get; set; }
    public SnapshotStatus Result { get; set; }
}