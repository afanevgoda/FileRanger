using AutoMapper;
using Common.Enum;
using DAL.DB;
using DAL.Repositories;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace FileBrowser.Services;

public class FileServiceImpl : FileService.FileServiceBase{
    private readonly IMapper _mapper;
    private readonly IRepository<DAL.Models.File> _fileRepo;
    private readonly IRepository<DAL.Models.Snapshot> _snapshotRepo;

    public FileServiceImpl(IRepository<DAL.Models.File> fileRepo, IRepository<DAL.Models.Snapshot> snapshotRepo,
        IMapper mapper) {
        _fileRepo = fileRepo;
        _mapper = mapper;
        _snapshotRepo = snapshotRepo;
    }

    public override Task<ListOfFiles> GetFiles(GetFilesForSnapshot request, ServerCallContext context) {
        var files = _fileRepo.GetByCondition(file => file.SnapshotId == request.SnapshotId
                                                     && file.ParentPath == request.TargetPath);
        var mappedFiles = _mapper.Map<List<DAL.Models.File>, List<File>>(files);
        var result = new ListOfFiles();
        result.Files.AddRange(mappedFiles);
        return Task.FromResult(result);
    }

    public override Task<SimpleResponse> SaveFiles(ListOfFiles request, ServerCallContext context) {
        var snapshotId = Int32.Parse(request.Files.FirstOrDefault()?.SnapshotId);
        if (!_snapshotRepo.DoesExistWithId(snapshotId))
            return Task.FromResult(new SimpleResponse {
                //todo: toString -> int
                Result = GrpcSimpleResponse.NotFound
            });

        var snapshot = _snapshotRepo.Get(snapshotId);
        var mappedFiles = _mapper.Map<List<File>, List<DAL.Models.File>>(request.Files.ToList());
        _fileRepo.AddRange(mappedFiles);

        return Task.FromResult(new SimpleResponse() {
            Result = GrpcSimpleResponse.Ok
        });
    }
}