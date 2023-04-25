namespace CarsMongoDbDemo.ViewModels.XlsxFile;

public record XlsxFileViewModel
{
    public string XlsxFileName { get; init; } = string.Empty!;
    public DateTime XlsxDateAndTime { get; init; }
}