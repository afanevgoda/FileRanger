namespace FileScanner.WebAppCommunication;

public interface ICallouter{
    Task Callout();

    string GetHostName();
}