namespace CarsMongoDbDemo.Models;

public class CarStoreDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string CarsCollectionName { get; set; } = null!;
}