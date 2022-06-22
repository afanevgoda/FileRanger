using AutoMapper;
using Common.Enum;
using DAL.DB;
using DAL.Models;
using DAL.Repositories;
using Grpc.Core;
using Grpc.Core.Utils;
using FolderEntity = DAL.Models.Folder;

namespace FileBrowser.Services;

public class FolderServiceImpl : FolderService.FolderServiceBase{
    private readonly IMapper _mapper;
    private readonly IRepository<Snapshot> _snapshotRepo;
    private readonly IRepository<FolderEntity> _folderRepo;

    public FolderServiceImpl(IRepository<Snapshot> snapshotRepo, IRepository<DAL.Models.Folder> folderRepo, IMapper mapper) {
        _snapshotRepo = snapshotRepo;
        _folderRepo = folderRepo;
        _mapper = mapper;
    }

    public override Task<SimpleResponse> SaveFolders(ListOfFolders request, ServerCallContext context) {
        var folders = _mapper.Map<List<Folder>, List<DAL.Models.Folder>>(request.Folder.ToList());

        var snapshotId = folders.FirstOrDefault()?.SnapshotId;
        if (!_snapshotRepo.DoesExistWithId(snapshotId))
            return Task.FromResult(new SimpleResponse() {
                //todo: toString -> int
                Result = GrpcSimpleResponse.NotFound
            });

        _folderRepo.AddRange(folders);
        return Task.FromResult(new SimpleResponse() {
            //todo: toString -> int
            Result = GrpcSimpleResponse.Ok
        });
    }

    public override Task<ListOfFolders> GetFolders(GetFolderForSnapshot request, ServerCallContext context) {
        var result = new ListOfFolders();
        var subFolders = _folderRepo.GetByCondition(folder => folder.ParentPath == request.TargetPath
                                             && folder.SnapshotId == request.SnapshotId);
        var folders = _mapper.Map<List<DAL.Models.Folder>, List<Folder>>(subFolders);
        result.Folder.AddRange(folders);

        return Task.FromResult(result);
    }
}