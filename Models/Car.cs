namespace CarsMongoDbDemo.Models;

public class Car
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("Brand")] public string VehicleBrand { get; set; } = string.Empty!;

    public string Name { get; set; } = string.Empty!;
    public int Price { get; set; }
}