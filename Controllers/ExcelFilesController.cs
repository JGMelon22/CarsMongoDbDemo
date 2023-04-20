using CarsMongoDbDemo.Services;

namespace CarsMongoDbDemo.Controllers;

public class ExcelFilesController : Controller
{
    // DI
    private readonly CarsService _carsService;

    public ExcelFilesController(CarsService carsService)
    {
        _carsService = carsService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var fileService = new FileService(_carsService);
        var files = await fileService.GetXlsxFile();
        return await Task.Run(() => View(files));
    }

    [HttpGet]
    public IActionResult DeleteFile(string fileName)
    {
        var fileService = new FileService(_carsService);
        fileService.DeleteFile(fileName);

        return RedirectToAction("Index", "ExcelFiles");
    }

    [HttpGet]
    public async Task<IActionResult> CreateFile()
    {
        var fileService = new FileService(_carsService);
        await fileService.CreateReport();

        return RedirectToAction("Index", "ExcelFiles");
    }
}