using System.ComponentModel.DataAnnotations;

namespace CarsMongoDbDemo.ViewModels.Car;

public class UpdateCarViewModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("Brand")] public string VehicleBrand { get; set; } = string.Empty!;

    public string Name { get; set; } = string.Empty!;

    [Range(2000, 300000, ErrorMessage = "Car Price must be between $2000 and $300000!")]
    public int Price { get; set; }
}