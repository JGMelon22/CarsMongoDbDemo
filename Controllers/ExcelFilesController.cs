using CarsMongoDbDemo.Services;

namespace CarsMongoDbDemo.Controllers;

public class ExcelFilesController : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        FileService fileService = new FileService();
        var files  = await fileService.GetXlsxFile();
        return await Task.Run(() => View(files));
    }
}