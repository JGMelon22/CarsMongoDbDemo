using CarsMongoDbDemo.Interfaces;
using CarsMongoDbDemo.ViewModels.Car;

namespace CarsMongoDbDemo.Services;

public class SortingService
{
    private readonly ICarsService _carsService;

    public SortingService(ICarsService carsService)
    {
        _carsService = carsService;
    }

    public async Task<List<CarResultViewModel>> SortCars(string sortOrder)
    {
        var cars = await _carsService.GetCarsAsync();
        var mappedCars = cars.Data
            .Select(x => new CarResultViewModel
            {
                Id = x.Id,
                VehicleBrand = x.VehicleBrand,
                Name = x.Name,
                Price = x.Price
            })
            .ToList()
            .OrderBy(x => x.VehicleBrand);

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