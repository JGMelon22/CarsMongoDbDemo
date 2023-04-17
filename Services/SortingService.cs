using CarsMongoDbDemo.ViewModels.Car;

namespace CarsMongoDbDemo.Services;

public class SortingService
{
    private readonly CarsService _carsService;

    public SortingService(CarsService carsService)
    {
        _carsService = carsService;
    }

    public async Task<List<GetCarViewModel>> SortCars(string sortOrder)
    {
        var cars = await _carsService.GetCarsAsync();
        var mappedCars = cars.Select(x => new GetCarViewModel
        {
            Id = x.Id,
            VehicleBrand = x.VehicleBrand,
            Name = x.Name,
            Price = x.Price
        }).Take(15).ToList().OrderBy(x => x.Id);

        switch (sortOrder)
        {
            case "brand_desc":
                mappedCars = mappedCars.OrderByDescending(x => x.VehicleBrand);
                break;

            default:
                mappedCars = mappedCars.OrderBy(x => x.Id);
                break;
        }

        return mappedCars.ToList();
    }
}