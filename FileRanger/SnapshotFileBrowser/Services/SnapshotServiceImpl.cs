using AutoMapper;
using Common.Enum;
using Common.Snapshot;
using DAL.Models;
using DAL.Repositories;
using Grpc.Core;
using Helpers.Extensions;

namespace FileBrowser.Services;

public class SnapshotServiceImpl : SnapshotService.SnapshotServiceBase{
    private readonly IMapper _mapper;
    private readonly IRepository<Snapshot> _snapshotRepo;

    public SnapshotServiceImpl(IRepository<Snapshot> snapshotRepo, IMapper mapper) {
        _snapshotRepo = snapshotRepo;
        _mapper = mapper;
    }

    public override Task<SnapshotId> AddNewSnapshot(NewSnapshot request, ServerCallContext context) {
        var snapshot = new Snapshot {
            CreatedAt = DateTime.Parse(request.CreatedAt).ToUtcKind(),
            Drive = request.Drive,
            Hostname = request.HostName,
            Result = SnapshotStatus.InProgress
        };
        var addedSnapshot = _snapshotRepo.Add(snapshot);
        return Task.FromResult(new SnapshotId {
            SnapshotId_ = addedSnapshot.Id
        });
    }

    public override Task<ListOfSnapshots> GetSnapshots(GetSnapshotMessage request, ServerCallContext context) {
        var snapshots = _snapshotRepo.GetByCondition(snapshot => snapshot.Drive == request.TargetDrive
                                                                 && snapshot.Hostname == request.TargetHostName);
        var result = new ListOfSnapshots();
        var mappedSnapshots = _mapper.Map<List<Snapshot>, List<SnapshotMessage>>(snapshots.ToList());
        result.Snapshots.AddRange(mappedSnapshots);
        return Task.FromResult(result);
    }

    public override Task<Response> DeleteSnapshot(SnapshotId request, ServerCallContext context) {
        var targetSnapshot = _snapshotRepo.Get(request.SnapshotId_);
        if (targetSnapshot == null) {
            return Task.FromResult(new Response {
                Result = GrpcResult.NOT_FOUND.ToString()
            });
        }

        _snapshotRepo.Delete(targetSnapshot);
        return Task.FromResult(new Response {
            Result = GrpcResult.OK.ToString()
        });
    }

    public override Task<Response> FinishSnapshot(SnapshotResult request, ServerCallContext context) {
        var targetSnapshot = _snapshotRepo.Get(request.SnapshotId);
        if (targetSnapshot == null) {
            return Task.FromResult(new Response {
                Result = GrpcResult.NOT_FOUND.ToString()
            });
        }

        targetSnapshot.Result = (SnapshotStatus)request.Result;
        _snapshotRepo.Update(targetSnapshot);
        return Task.FromResult(new Response {
            Result = GrpcResult.OK.ToString()
        });
    }
}