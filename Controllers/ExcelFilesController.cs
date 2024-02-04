using CarsMongoDbDemo.Interfaces;
using CarsMongoDbDemo.Services;

namespace CarsMongoDbDemo.Controllers;

public class ExcelFilesController : Controller
{
    private readonly ICarsService _carsService;

    public ExcelFilesController(ICarsService carsService)
    {
        _carsService = carsService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var fileService = new FileService(_carsService);
        var files = fileService.GetXlsxFile();
        return View(files);
    }

    [HttpGet]
    public async Task<IActionResult> CreateFile()
    {
        var fileService = new FileService(_carsService);
        await fileService.CreateReport();

        return RedirectToAction("Index", "ExcelFiles");
    }

    [HttpGet]
    public async Task<IActionResult> DownloadFile(string fileName)
    {
        var fileService = new FileService(_carsService);
        var bytes = await fileService.DownloadFile(fileName);

        return File(bytes, "application/octet-steam", fileName);
    }

    [HttpGet]
    public IActionResult DeleteFile(string fileName)
    {
        var fileService = new FileService(_carsService);
        fileService.DeleteFile(fileName);

        return RedirectToAction("Index", "ExcelFiles");
    }
}