using DtoLibrary.Snapshot.GRPC;

namespace FileScanner.WebAppCommunication;

public interface IDataSender{
    Task SendFolderData(List<Folder> newFolders);
    
    Task SendFilesData(List<DtoLibrary.Snapshot.GRPC.File> newFolders);

    Task<int> SendNewSnapshot(AddNewSnapshot snapshot);
}