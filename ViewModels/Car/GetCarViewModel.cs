namespace CarsMongoDbDemo.ViewModels.Car;

public record GetCarViewModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; init; }

    [BsonElement("Brand")]
    public string VehicleBrand { get; init; } = string.Empty!;

    public string Name { get; init; } = string.Empty!;
    public int Price { get; init; }
}