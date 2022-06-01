using AutoMapper;
using Common.Snapshot.Http;

namespace WebApp.Automapper;

public class MapperProfile : Profile{
    public MapperProfile() {
        CreateMap<Folder, Common.Snapshot.GRPC.Folder>();
        CreateMap<Common.Snapshot.GRPC.Folder, Folder>();
        CreateMap<File, Common.Snapshot.GRPC.File>();
        CreateMap<Common.Snapshot.GRPC.File, File>();
        CreateMap<SnapshotMessage, SnapshotDto>();
        CreateMap<SnapshotDto, SnapshotMessage>();
    }
}