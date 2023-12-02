namespace CarsMongoDbDemo.ViewModels.XlsxFile;

public record XlsxFileViewModel
{
    public required string XlsxFileName { get; init; }
    public DateTime XlsxDateAndTime { get; init; }
}