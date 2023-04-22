using CarsMongoDbDemo.ViewModels.XlsxFile;
using MiniExcelLibs;

namespace CarsMongoDbDemo.Services;

public class FileService
{
    private readonly CarsService _carsService;

    public FileService(CarsService carsService)
    {
        _carsService = carsService;
    }

    public async Task<List<XlsxFileViewModel>> GetXlsxFile()
    {
        var path = Path.Combine(Environment.CurrentDirectory, "Files");

        if (!Directory.Exists(path)) Directory.CreateDirectory(path);

        var filePath = Directory.GetFiles(path);

        // If find any file, create a list with it
        var excelFiles = new List<XlsxFileViewModel>();

        foreach (var item in filePath)
            excelFiles.Add(new XlsxFileViewModel
            {
                XlsxFileName = Path.GetFileName(item)
            });

        return await Task.FromResult(excelFiles.ToList());
    }

    // Create report
    public async Task CreateReport()
    {
        var folderPath = Path.Combine(Environment.CurrentDirectory, "Files" + Path.DirectorySeparatorChar,
            $"{Guid.NewGuid()}.xlsx");

        var data = await _carsService.GetCarsAsync();
        var rowsData = data.Select(item => new
            { item.Id, item.Name, item.VehicleBrand, Price = item.Price.ToString() });

        await MiniExcel.SaveAsAsync(folderPath, rowsData);
    }

    // Download file
    public async Task<byte[]> DownloadFile(string fileName)
    {
        var path = Path.Combine(Path.Combine(Environment.CurrentDirectory, "Files" + Path.DirectorySeparatorChar) +
                                fileName);

        // Le o arquivo
        var bytes = await File.ReadAllBytesAsync(path);
        return await Task.FromResult(bytes);
    }

    public void DeleteFile(string fileName)
    {
        // File to remove path
        var path = Path.Combine(Path.Combine(Environment.CurrentDirectory, "Files" + Path.DirectorySeparatorChar) +
                                fileName);

        if (File.Exists(path))
            File.Delete(path);
    }
}