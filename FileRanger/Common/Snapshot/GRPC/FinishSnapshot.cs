namespace Common.Snapshot.GRPC;

public class FinishSnapshot{
    public SnapshotStatus Status { get; set; }
    public int SnapshotId { get; set; }
}