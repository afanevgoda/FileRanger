using DAL.Models;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using DtoLibrary.Snapshot.GRPC;

namespace WebApp.Controllers;

public class SnapshotController : Controller{
    private readonly GrpcChannel _channel;

    public SnapshotController(GrpcChannel channel) {
        _channel = channel;
    }

    [HttpPost]
    public async Task<int> AddNewSnapshot([FromBody] AddNewSnapshot newSnapshot) {
        var client = new SnapshotService.SnapshotServiceClient(_channel);
        var message = new NewSnapshot {
            CreatedAt = newSnapshot.CreatedAt,
            Drive = newSnapshot.TargetDrive,
            HostName = newSnapshot.HostName
        };
        var reply = await client.AddNewSnapshotAsync(message);
        return reply.SnapshotId;
    }

    [HttpGet]
    public async Task<List<SnapshotMessage>> GetSnapshots([FromQuery] string hostName, [FromQuery] string drive) {
        var client = new SnapshotService.SnapshotServiceClient(_channel);
        var message = new GetSnapshotMessage {
            TargetHostName = hostName,
            TargetDrive = drive.Replace(":\\", "")
        };
        var reply = await client.GetSnapshotsAsync(message);
        return reply.Snapshots.ToList();
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
}