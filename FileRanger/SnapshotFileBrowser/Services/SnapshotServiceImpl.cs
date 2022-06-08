using AutoMapper;
using Common.Enum;
using Common.Snapshot;
using DAL.DB;
using DAL.Models;
using Grpc.Core;
using Helpers.Extensions;

namespace FileBrowser.Services;

public class SnapshotServiceImpl : SnapshotService.SnapshotServiceBase{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public SnapshotServiceImpl(AppDbContext dbContext, IMapper mapper) {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public override Task<SnapshotId> AddNewSnapshot(NewSnapshot request, ServerCallContext context) {
        var snapshot = new Snapshot {
            CreatedAt = DateTime.Parse(request.CreatedAt).ToUtcKind(),
            Drive = request.Drive,
            Hostname = request.HostName,
            Result = SnapshotStatus.InProgress
        };
        var addedSnapshot = _dbContext.Snapshots.Add(snapshot);
        _dbContext.SaveChanges();
        return Task.FromResult(new SnapshotId {
            SnapshotId_ = addedSnapshot.Entity.Id
        });
    }

    public override Task<ListOfSnapshots> GetSnapshots(GetSnapshotMessage request, ServerCallContext context) {
        var snapshots = _dbContext.Snapshots.Where(x =>
            x.Drive == request.TargetDrive
            && x.Hostname == request.TargetHostName);

        var result = new ListOfSnapshots();
        var mappedSnapshots = _mapper.Map<List<Snapshot>, List<SnapshotMessage>>(snapshots.ToList());
        result.Snapshots.AddRange(mappedSnapshots);
        return Task.FromResult(result);
    }

    public override Task<Response> DeleteSnapshot(SnapshotId request, ServerCallContext context) {
        var targetSnapshot = _dbContext.Snapshots.FirstOrDefault(x => x.Id == request.SnapshotId_);
        if (targetSnapshot == null) {
            return Task.FromResult(new Response {
                Result = GrpcResult.NOT_FOUND.ToString()
            });    
        }
        _dbContext.Snapshots.Remove(targetSnapshot);
        _dbContext.SaveChanges();
        return Task.FromResult(new Response {
            Result = GrpcResult.OK.ToString()
        });
    }

    public override Task<Response> FinishSnapshot(SnapshotResult request, ServerCallContext context) {
        var targetSnapshot = _dbContext.Snapshots.FirstOrDefault(x => x.Id == request.SnapshotId);
        if (targetSnapshot == null) {
            return Task.FromResult(new Response {
                Result = GrpcResult.NOT_FOUND.ToString()
            });    
        }
        targetSnapshot.Result = (SnapshotStatus)request.Result;
        _dbContext.SaveChanges();
        return Task.FromResult(new Response {
            Result = GrpcResult.OK.ToString()
        });
    }
}