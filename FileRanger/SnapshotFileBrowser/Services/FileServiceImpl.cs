using AutoMapper;
using Common.Enum;
using DAL.DB;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace FileBrowser.Services;

public class FileServiceImpl : FileService.FileServiceBase{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public FileServiceImpl(AppDbContext dbContext, IMapper mapper) {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public override Task<ListOfFiles> GetFiles(GetFilesForSnapshot request, ServerCallContext context) {
        var files = _dbContext.Files
            .Where(x => x.SnapshotId == request.SnapshotId && x.ParentPath == request.TargetPath)
            .ToList();
        var mappedFiles = _mapper.Map<List<DAL.Models.File>, List<File>>(files);
        var result = new ListOfFiles();
        result.Files.AddRange(mappedFiles);
        return Task.FromResult(result);
    }

    public override Task<Response> SaveFiles(ListOfFiles request, ServerCallContext context) {
        var mappedFiles = _mapper.Map<List<File>, List<DAL.Models.File>>(request.Files.ToList());
        _dbContext.Files.AddRange(mappedFiles);
        _dbContext.SaveChanges();

        return Task.FromResult(new Response() {
            Result = GrpcResult.OK.ToString()
        });
    }
}