using System.Net.Mime;
using System.Text;
using System.Text.Json;
using DtoLibrary.Snapshot.GRPC;
using FileGrpcDto = DtoLibrary.Snapshot.GRPC.File;

namespace FileScanner.WebAppCommunication;

public class DataSender : IDataSender{
    private readonly IConfiguration _configuration;

    private readonly IHttpClientFactory _httpFactory;
    // private readonly ILogger _logger;

    public DataSender(IConfiguration configuration, IHttpClientFactory httpFactory) {
        _configuration = configuration;
        _httpFactory = httpFactory;
    }

    public async Task SendFolderData(List<Folder> newFolders) {
        var message = new StringContent(
            JsonSerializer.Serialize(newFolders),
            Encoding.UTF8,
            MediaTypeNames.Application.Json);

        var httpClient = _httpFactory.CreateClient();
        var httpResponseMessage = await httpClient
            .PostAsync($"{_configuration.GetSection("WebAppHost").Value}/Scanner/AddFolderData", message);
    }

    public async Task SendFilesData(List<FileGrpcDto> newFiles) {
        var message = new StringContent(
            JsonSerializer.Serialize(newFiles),
            Encoding.UTF8,
            MediaTypeNames.Application.Json);

        var httpClient = _httpFactory.CreateClient();
        var httpResponseMessage = await httpClient
            .PostAsync($"{_configuration.GetSection("WebAppHost").Value}/Scanner/AddFilesData", message);
    }

    public async Task<int> SendNewSnapshot(AddNewSnapshot snapshot) {
        var message = new StringContent(
            JsonSerializer.Serialize(snapshot),
            Encoding.UTF8,
            MediaTypeNames.Application.Json);

        var httpClient = _httpFactory.CreateClient();
        var httpResponseMessage = await httpClient
            .PostAsync($"{_configuration.GetSection("WebAppHost").Value}/Snapshot/AddNewSnapshot", message);
        StreamReader readStream = new StreamReader(httpResponseMessage.Content.ReadAsStream(), Encoding.UTF8);
        return Int32.Parse(readStream.ReadToEnd());
    }
}