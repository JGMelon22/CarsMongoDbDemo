using CarsMongoDbDemo.ViewModels.XlsxFile;

namespace CarsMongoDbDemo.Services;

public class FileService
{
    public async Task<List<XlsxFileViewModel>> GetXlsxFile()
    {
        var path = Path.Combine(Environment.CurrentDirectory, "Files");

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        var filePath = Directory.GetFiles(path);

        // If find any file, create a list with it
        var excelFiles = new List<XlsxFileViewModel>();

        foreach (var item in filePath)
        {
            excelFiles.Add(new XlsxFileViewModel()
            {
                XlsxFileName = Path.GetFileName(item)
            });
        }

        return await Task.FromResult(excelFiles.ToList());
    }
}