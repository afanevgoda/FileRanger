using FileRanger.DAL.Models;
using FileRanger.Services;
using Microsoft.AspNetCore.Mvc;
using File = FileRanger.DAL.Models.File;

namespace FileRanger.Controllers;

[Route("[controller]/[action]")]
public class FileBrowserController{
    private FileBrowser _browser;

    public FileBrowserController(FileBrowser browser) {
        _browser = browser;
    }

    [HttpGet]
    public List<Folder> Folders([FromQuery] string targetPath) {
        return _browser.GetSubFolders(targetPath);
    }
    
    [HttpGet]
    public List<File> Files([FromQuery] string targetPath) {
        return _browser.GetFilesForFolder(targetPath);
    }
}