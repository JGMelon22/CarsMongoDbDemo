namespace CarsMongoDbDemo.ViewModels.Car;

public record GetCarViewModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; init; }

    [BsonElement("Brand")] public required string VehicleBrand { get; init; } 

    public required string Name { get; init; } 
    public double Price { get; init; }
}