namespace CarsMongoDbDemo.Models;

public class Car
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; } 

    [BsonElement("Brand")] public required string VehicleBrand { get; set; }

    public required string Name { get; set; } 
    public double Price { get; set; }
}