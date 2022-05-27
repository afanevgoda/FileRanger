using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace FileScanner.WebAppCommunication;

public class Callouter : ICallouter{
    private readonly IHttpClientFactory _httpFactory;
    private readonly IConfiguration _configuration;

    public Callouter(IHttpClientFactory httpFactory, IConfiguration configuration) {
        _httpFactory = httpFactory;
        _configuration = configuration;
    }

    public async Task Callout() {
        var message = new StringContent(
            JsonSerializer.Serialize(new {
                HostName = GetHostName(),
                Drives = GetAvailableDrives()
            }),
            Encoding.UTF8,
            MediaTypeNames.Application.Json);

        var httpClient = _httpFactory.CreateClient();
        await httpClient
            .PostAsync($"{_configuration.GetSection("WebAppHost").Value}/Scanner/Callout", message);
    }

    public string GetHostName() {
        return Environment.MachineName;
    }

    private List<string> GetAvailableDrives() {
        return DriveInfo.GetDrives().Select(x => x.Name).ToList();
    }
}