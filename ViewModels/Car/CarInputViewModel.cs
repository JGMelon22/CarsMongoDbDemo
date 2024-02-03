namespace CarsMongoDbDemo.ViewModels.Car;

public record CarInputViewModel
{
    [BsonElement("Brand")] public required string VehicleBrand { get; init; }
    public required string Name { get; init; }
    public double Price { get; init; }
}