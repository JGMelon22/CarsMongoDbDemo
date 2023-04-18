using System.ComponentModel.DataAnnotations;

namespace CarsMongoDbDemo.ViewModels.Car;

public class AddCarViewModel
{
    [BsonElement("Brand")]
    [Required(ErrorMessage = "Car Brand must be informed!")]
    [MinLength(1, ErrorMessage = "Car Brand can't contain less than 1 character!")]
    [MaxLength(100, ErrorMessage = "Car Brand can't exceed 100 characters!")]
    public string VehicleBrand { get; set; } = string.Empty!;

    [Required(ErrorMessage = "Car Name must be informed!")]
    [MinLength(1, ErrorMessage = "Car Name can't contain less than 1 character!")]
    [MaxLength(100, ErrorMessage = "Car Name can't exceed 100 characters!")]
    public string Name { get; set; } = string.Empty!;

    [Required(ErrorMessage = "Car Price must be informed!")]
    [Range(2000, 300000, ErrorMessage = "Car Price must be between $2000 and $300000!")]
    public int Price { get; set; }
}