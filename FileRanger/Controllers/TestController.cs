using FileRanger.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using File = FileRanger.DAL.Models.File;

namespace FileRanger.Controllers;

public class TestController : Controller{

    private IRepository<File> _fileRepo;

    public TestController(IRepository<File> fileRepo) {
        _fileRepo = fileRepo;
    }
    
    [HttpGet]
    public List<File> GetAll() {
        return _fileRepo.GetAll();
    }
    
    [HttpPost]
    public void AddNew([FromBody] File entity) {
        _fileRepo.Add(entity);
    }
}