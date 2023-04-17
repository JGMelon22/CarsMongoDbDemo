using System.ComponentModel.DataAnnotations;

namespace CarsMongoDbDemo.ViewModels.Car;

public class AddCarViewModel
{
    [BsonElement("Brand")]
    [Required(ErrorMessage = "Car Brand must be informed!")]
    public string VehicleBrand { get; set; } = string.Empty!;

    [Required(ErrorMessage = "Car Name must be informed!")]
    public string Name { get; set; } = string.Empty!;

    [Required(ErrorMessage = "Car Price must be informed!")]
    [Range(2000, 300000, ErrorMessage = "Car Price must be between $2000 and $300000!")]
    public int Price { get; set; }
}