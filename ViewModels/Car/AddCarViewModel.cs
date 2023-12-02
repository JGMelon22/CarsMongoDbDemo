namespace CarsMongoDbDemo.ViewModels.Car;

public record AddCarViewModel
{
    [BsonElement("Brand")] public required string VehicleBrand { get; init; }
    
    public required string Name { get; init; } 
    public double Price { get; init; }
}