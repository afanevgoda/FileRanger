using AutoMapper;
using Common.Enum;
using DAL.DB;
using Grpc.Core;
using Grpc.Core.Utils;
using FolderEntity = DAL.Models.Folder;

namespace FileBrowser.Services;

public class FolderServiceImpl : FolderService.FolderServiceBase{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public FolderServiceImpl(AppDbContext dbContext, IMapper mapper) {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public override Task<Response> SaveFolders(ListOfFolders request, ServerCallContext context) {
        var folders = _mapper.Map<List<Folder>, List<DAL.Models.Folder>>(request.Folder.ToList());
        _dbContext.Folders.AddRange(folders);
        _dbContext.SaveChanges();

        return Task.FromResult(new Response() {
            //todo: toString -> int
            Result = GrpcResult.OK.ToString()
        });
    }

    public override Task<ListOfFolders> GetFolders(GetFolderForSnapshot request, ServerCallContext context) {
        var result = new ListOfFolders();
        var subFolders = _dbContext.Folders.Where(x =>
            x.ParentPath == request.TargetPath
            && x.SnapshotId == request.SnapshotId).ToList();
        var folders = _mapper.Map<List<DAL.Models.Folder>, List<Folder>>(subFolders);
        result.Folder.AddRange(folders);

        return Task.FromResult(result);
    }
}