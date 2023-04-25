namespace CarsMongoDbDemo.ViewModels.Car;

public record AddCarViewModel
{
    [BsonElement("Brand")]
    public string VehicleBrand { get; init; } = string.Empty!;
    public string Name { get; init; } = string.Empty!;
    public int Price { get; init; }
}