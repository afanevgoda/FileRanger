using AutoMapper;
using DAL.Models;

namespace FileBrowser.AutoMapper;

public class AutomapperProfile : Profile{
    public AutomapperProfile() {
        // source -> target
        CreateMap<DAL.Models.Folder, Folder>();
        CreateMap<Folder, DAL.Models.Folder>();
        CreateMap<Snapshot, SnapshotMessage>()
            .ForMember(
                x => x.CreatedAt,
                opt => opt.MapFrom(src => src.CreatedAt.ToString("O")));
        CreateMap<SnapshotMessage, Snapshot>();
        CreateMap<File, DAL.Models.File>();
        CreateMap<DAL.Models.File, File>();
    }
}