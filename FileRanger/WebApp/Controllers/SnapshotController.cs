using AutoMapper;
using Common.Snapshot;
using Common.Snapshot.GRPC;
using Common.Snapshot.Http;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class SnapshotController : Controller{
    private readonly GrpcChannel _channel;
    private readonly IMapper _mapper;

    public SnapshotController(GrpcChannel channel, IMapper mapper) {
        _channel = channel;
        _mapper = mapper;
    }

    [HttpPut]
    public async Task<int> AddNewSnapshot([FromBody] AddNewSnapshot newSnapshot) {
        var client = new SnapshotService.SnapshotServiceClient(_channel);
        var message = new NewSnapshot {
            CreatedAt = newSnapshot.CreatedAt,
            Drive = newSnapshot.TargetDrive,
            HostName = newSnapshot.HostName
        };
        var reply = await client.AddNewSnapshotAsync(message);
        return reply.SnapshotId_;
    }

    [HttpPost]
    public async Task<string> SendSnapshotResult([FromBody] FinishSnapshot snapshotStatus) {
        var client = new SnapshotService.SnapshotServiceClient(_channel);
        var message = new SnapshotResult {
            Result = (Result)snapshotStatus.Status,
            SnapshotId = snapshotStatus.SnapshotId
        };

        var reply = await client.FinishSnapshotAsync(message);
        return reply.Result;
    }

    [HttpGet]
    public async Task<List<SnapshotDto>> GetSnapshots([FromQuery] string hostName, [FromQuery] string drive) {
        var client = new SnapshotService.SnapshotServiceClient(_channel);
        var message = new GetSnapshotMessage {
            TargetHostName = hostName,
            TargetDrive = drive.Replace(":\\", "")
        };
        var reply = await client.GetSnapshotsAsync(message);
        var result = _mapper.Map<List<SnapshotMessage>, List<SnapshotDto>>(reply.Snapshots.ToList());
        return result;
    }

    [HttpGet]
    public async Task<List<Folder>> GetFolders(string targetPath, int snapshotId) {
        var client = new FolderService.FolderServiceClient(_channel);

        var reply = await client.GetFoldersAsync(new GetFolderForSnapshot {
            TargetPath = targetPath,
            SnapshotId = snapshotId
        });

        var result = reply.Folder.Select(x => new Folder {
            FullPath = x.FullPath,
            Id = x.Id,
            Name = x.Name,
            ParentPath = x.ParentPath
        }).ToList();

        return result;
    }

    [HttpGet]
    public async Task<List<File>> GetFiles(string targetPath, int snapshotId) {
        var client = new FileService.FileServiceClient(_channel);

        var reply = await client.GetFilesAsync(new GetFilesForSnapshot {
            TargetPath = targetPath,
            SnapshotId = snapshotId
        });

        var result = reply.Files.Select(x => new File {
            FullPath = x.FullPath,
            Id = x.Id,
            Name = x.Name,
            ParentPath = x.ParentPath
        }).ToList();

        return result;
    }

    [HttpDelete]
    public async Task<string> DeleteSnapshot(int snapshotId) {
        var client = new SnapshotService.SnapshotServiceClient(_channel);
        
        var reply = await client.DeleteSnapshotAsync(new SnapshotId() {
            SnapshotId_ = snapshotId
        });

        if (reply.Result == "NOT_FOUND")
            Response.StatusCode = 404;
        
        return reply.Result;
    }
}